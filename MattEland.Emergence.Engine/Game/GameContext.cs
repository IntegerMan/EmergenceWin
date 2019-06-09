using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.AI;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Loot;
using MattEland.Emergence.Engine.Messages;
using MattEland.Emergence.Engine.Services;
using MattEland.Emergence.Engine.Vision;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Game
{

    public sealed class GameContext
    {
        private readonly IList<GameMessage> _messages;

        public GameContext([NotNull] LevelData level,
                              [NotNull] GameService gameService,
                              [NotNull] EntityDataProvider entityService,
                              [NotNull] CombatManager combatManager,
                              [NotNull] LootProvider lootProvider, 
                              [NotNull] IRandomization randomizer)
        {
            GameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            EntityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
            CombatManager = combatManager ?? throw new ArgumentNullException(nameof(combatManager));
            LootProvider = lootProvider ?? throw new ArgumentNullException(nameof(lootProvider));
            Randomizer = randomizer  ?? throw new ArgumentNullException(nameof(randomizer));

            _messages = new List<GameMessage>();

            AI = new ArtificialIntelligenceService(this);

            SetLevel(level);
        }

        public IEnumerable<GameMessage> Messages => _messages;
        public IRandomization Randomizer { get; }

        public event EventHandler<ActorDamagedEventArgs> OnActorHurt;

        public EntityDataProvider EntityService { get; set; }

        public IEnumerable<GameCell> GetCellsVisibleFromPoint(Pos2D point, decimal radius)
        {
            var fovCalculator = new ShadowCasterViewProvider(Level);
            var visiblePositions = fovCalculator.ComputeFov(point, radius);

            foreach (var pos in visiblePositions)
            {
                var cell = Level.GetCell(pos);

                if (cell != null)
                {
                    yield return cell;
                }
            }
        }

        public void HandleObjectKilled(GameObjectBase defender, GameObjectBase attacker)
        {
            if (CanPlayerSee(defender.Pos))
            {
                AddEffect(new DestroyedEffect(defender));
            }

            // Make sure that it stays dead
            RemoveObject(defender);

            defender.OnDestroyed(this, attacker);
        }

        public void RemoveObject([NotNull] GameObjectBase gameObj)
        {
            if (gameObj == null) throw new ArgumentNullException(nameof(gameObj));

            Level.RemoveObject(gameObj);

            AddMessage(new DestroyedMessage(gameObj));
        }

        public void ReplaceObject([NotNull] GameObjectBase oldObj, [NotNull] GameObjectBase newObj)
        {
            if (oldObj == null) throw new ArgumentNullException(nameof(oldObj));

            newObj.Pos = oldObj.Pos;
            newObj.Id = oldObj.Id;

            Level.RemoveObject(oldObj);
            Level.AddObject(newObj);

            AddMessage(new ObjectUpdatedMessage(newObj));
        }

        public void AddEffect([NotNull] EffectBase effect)
        {
            if (effect == null) throw new ArgumentNullException(nameof(effect));

            AddMessage(effect);
        }

        public bool CanPlayerSee(GameObjectBase obj) => obj != null && CanPlayerSee(obj.Pos);

        public bool CanPlayerSee(Pos2D pos) => Player != null && Player.CanSee(pos);

        public void PreviewObjectHurt(GameObjectBase attacker, GameObjectBase defender, int damage, DamageType damageType)
        {
            OnActorHurt?.Invoke(this, new ActorDamagedEventArgs(attacker, defender, damage, damageType));
        }

        public void SetLevel(LevelData level)
        {
            Level = level;
            Player = Level.FindPlayer();
        }

        public void ReplacePlayer(Player newPlayer)
        {
            ReplaceObject(Player, newPlayer);

            Player = newPlayer;

            Level.Objects.OfType<CharacterSelectTile>().Each(UpdateObject);
        }

        public GameObjectBase AddObject(GameObjectBase obj)
        {
            Level.AddObject(obj);

            AddMessage(new CreatedMessage(obj));

            return obj;
        }

        public void DisplayHelp(GameObjectBase source, string helpTopic)
        {
            string message = GetMessageForTopic(helpTopic);

            if (string.IsNullOrWhiteSpace(message)) return;

            // If the source of the message is corrupt, randomize the casing
            if (source != null && source.IsCorrupted)
            {
                message = GetCorruptedHelpMessage(message);
            }

            AddEffect(new HelpTextEffect(source, message));
        }

        private string GetCorruptedHelpMessage(string message)
        {
            const string noiseChars = "!@#$%&?,";

            var sb = new StringBuilder();

            foreach (var c in message.ToLowerInvariant())
            {
                // Ignore spaces
                if (c == ' ')
                {
                    sb.Append(c);
                    continue;
                }

                switch (Randomizer.GetInt(0, 2))
                {
                    case 0: // Lower Case
                        sb.Append(c);
                        break;

                    case 1: // Upper Case
                        sb.Append(c.ToString().ToUpperInvariant());
                        break;

                    default: // Random noise character
                        sb.Append(noiseChars.GetRandomElement(Randomizer));
                        break;
                }
            }

            return sb.ToString();
        }

        private string GetMessageForTopic(string helpTopic)
        {
            var topic = helpTopic.ToLowerInvariant();

            if (topic.StartsWith("help_actor_"))
            {
                var definition = EntityService.GetItem(helpTopic.Substring(5));

                if (definition != null && !string.IsNullOrWhiteSpace(definition.HelpText))
                {
                    return definition.HelpText;
                }
            }

            switch (topic)
            {
                case "help_firewalls":
                    return "Exits are protected by a firewall. Capture every core on a machine in order to move on.";

                case "help_welcome":
                    return "You're an AI inside of a computer network. Travel between systems and escape to the Internet.";

                default:
                    throw new NotSupportedException($"Topic {helpTopic} is not implemented");
            }
        }

        public CombatManager CombatManager { get; }
        public LootProvider LootProvider { get; }

        [NotNull]
        public Player Player { get; private set; }

        public LevelData Level { get; private set; }

        public GameService GameService { get; }

        public void AddMessage(string message, ClientMessageType messageType)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                AddMessage(new DisplayTextMessage(message, messageType));
            }
        }

        public void AdvanceToNextLevel()
        {
            var levels = new List<LevelType>
            {
                LevelType.ClientWorkstation,
                LevelType.SmartFridge,
                LevelType.MessagingServer,
                LevelType.Bastion,
                LevelType.RouterGateway
            };

            var levelType = Level.Id;

            // If we've reached the end of the series of levels, it's time to win.
            if (levelType == levels.Last())
            {
                EndGame();
                return;
            }

            int levelIndex = levels.IndexOf(levelType);
            levelType = levels[levelIndex + 1];

            SwitchToLevel(levelType);
        }

        public void SwitchToLevel(LevelType levelType)
        {
            Player.ClearKnownCells();

            // Generate messages for the level pieces that need to go away
            Level.Objects.Where(o => !o.IsPlayer).EachSafe(RemoveObject);

            var nextLevel = GameService.GenerateLevel(new LevelGenerationParameters
            {
                LevelType = levelType,
                PlayerType = Player.PlayerType
            }, Player);

            SetLevel(nextLevel);

            // Generate messages for the level pieces that were just created
            Level.Objects.Where(o => !o.IsPlayer).EachSafe(CreatedObject);

            // Generate an update message on the player
            UpdateObject(Player);
        }

        private void EndGame() => IsGameOver = true;

        public bool IsGameOver { get; set; }

        [NotNull]
        public ArtificialIntelligenceService AI { get; }

        public void AddError(string message) => AddMessage(message, ClientMessageType.Assertion);

        public void MoveObject([NotNull] GameObjectBase obj, Pos2D newPos)
        {
            var oldPos = obj.Pos;

            Level.MoveObject(obj, newPos);

            AddMessage(new MovedMessage(obj, oldPos, newPos));
        }

        private void AddMessage([NotNull] GameMessage message)
        {
            if (message == null) throw new ArgumentNullException(nameof(message));

            _messages.Add(message);
        }

        public void UpdateObject([NotNull] GameObjectBase gameObject) => AddMessage(new ObjectUpdatedMessage(gameObject));

        public void CreatedObject([NotNull] GameObjectBase gameObject) => AddMessage(new CreatedMessage(gameObject));

        public void TeleportActor([NotNull] Actor actor, Pos2D pos)
        {
            const int damage = 1;

            // Ensure that the teleport is allowed to happen - don't allow teleporting into walls
            var targetCell = Level.GetCell(pos);
            if (targetCell == null || targetCell.HasNonActorObstacle)
            {
                HandleFailedTeleport(actor, pos, damage);
                return;
            }

            // See who else is here before the action occurs
            var telefragged = Level.Actors.Where(a => a.Pos == pos).ToList();
            var oldPos = actor.Pos;

            // Add an effect for the teleportation
            if (CanPlayerSee(actor.Pos) || CanPlayerSee(pos) || actor.IsPlayer)
            {
                AddTeleportEffect(actor, pos);
            }

            // Actually move the actor
            Level.MoveObject(actor, pos);

            if (telefragged.Any())
            {
                HandleTelefragged(actor, telefragged, oldPos, pos, damage);
            }

        }

        private void HandleFailedTeleport([NotNull] Actor actor, Pos2D pos, int damage)
        {
            if (actor.IsPlayer || CanPlayerSee(pos))
            {
                AddMessage($"{actor.Name} tries to teleport but is blocked, causing {damage} scramble damage.",
                    ClientMessageType.Failure);
            }

            CombatManager.HurtObject(this, actor, actor, damage, "scrambles", DamageType.Normal);
        }

        private void AddTeleportEffect([NotNull] Actor actor, Pos2D pos)
        {
            // Don't give the client-application an unfair idea of where the target teleported to if they can't see it
            var endPos = pos;
            if (!actor.IsPlayer && !CanPlayerSee(pos))
            {
                endPos = new Pos2D(-500, -500);
            }

            AddEffect(new TeleportEffect(actor.Pos, endPos));
        }

        private void HandleTelefragged([NotNull] Actor actor,
            IEnumerable<Actor> telefragged,
            Pos2D oldPos,
            Pos2D newPos,
            int damage)
        {
            string hurtMessage;

            // Teleporting on top of another actor should always cause damage to that actor and swap it to your old location
            foreach (var target in telefragged)
            {
                hurtMessage = CombatManager.HurtObject(this, actor, target, damage, "scrambles", DamageType.Normal);

                if (actor.IsPlayer || target.IsPlayer || CanPlayerSee(newPos))
                {
                    AddMessage(hurtMessage, ClientMessageType.Generic);
                }

                Level.MoveObject(target, oldPos);
            }
            
            // If any collisions occurred, also hurt the actor who triggered it
            hurtMessage = CombatManager.HurtObject(this, actor, actor, damage, "scrambles", DamageType.Normal);

            if (actor.IsPlayer || Player.CanSee(newPos))
            {
                AddMessage(hurtMessage, ClientMessageType.Generic);
            }
        }

        public IEnumerable<Pos2D> CalculateLineOfSight([NotNull] Actor actor)
        {
            var fov = new ShadowCasterViewProvider(Level);
            fov.ComputeFov(actor.Pos, actor.EffectiveLineOfSightRadius);

            actor.VisibleCells = fov.VisiblePositions;
            actor.MarkCellsAsKnown(fov.VisiblePositions);

            return actor.VisibleCells;
        }

        public void AddSoundEffect(OpenableGameObjectBase source, SoundEffects sound) => AddEffect(new SoundEffect(source, sound));

        public void ClearMessages() => _messages.Clear();

        public void GenerateFillerWallsAsNeeded(Pos2D position)
        {
            var borderingPositions = new List<Pos2D>
            {
                position.Add(0, 1),
                position.Add(1, 0),
                position.Add(0, -1),
                position.Add(-1, 0)
            };

            foreach (var borderingPosition in borderingPositions)
            {
                if (Level.GetCell(borderingPosition) == null)
                {
                    var wall = GameObjectFactory.CreateWall(borderingPosition, Level.IsPosExterior(borderingPosition));
                    Level.AddCell(new GameCell { Pos = borderingPosition});
                    AddObject(wall);
                }
            }
            
        }

    }

}