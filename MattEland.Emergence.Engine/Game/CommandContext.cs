﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Loot;
using MattEland.Emergence.Engine.Model.Messages;
using MattEland.Emergence.Engine.Services;
using MattEland.Emergence.Engine.Vision;

namespace MattEland.Emergence.Engine.Game
{

    public sealed class CommandContext
    {
        private readonly IList<GameMessage> _messages;

        public CommandContext([NotNull] LevelData level,
                              [NotNull] GameService gameService,
                              [NotNull] EntityDefinitionService entityService,
                              [NotNull] CombatManager combatManager,
                              [NotNull] LootProvider lootProvider)
        {
            GameService = gameService ?? throw new ArgumentNullException(nameof(gameService));
            EntityService = entityService ?? throw new ArgumentNullException(nameof(entityService));
            CombatManager = combatManager ?? throw new ArgumentNullException(nameof(combatManager));
            LootProvider = lootProvider ?? throw new ArgumentNullException(nameof(lootProvider));

            _messages = new List<GameMessage>();

            SetLevel(level);
        }

        public IEnumerable<GameMessage> Messages => _messages;
        public IRandomization Randomizer { get; set; }

        public event EventHandler<ActorDamagedEventArgs> OnActorHurt;

        public EntityDefinitionService EntityService { get; set; }

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

        public void AddEffect([NotNull] EffectBase effect)
        {
            if (effect == null) throw new ArgumentNullException(nameof(effect));

            AddMessage(effect);
        }

        public bool CanPlayerSee(GameObjectBase obj)
        {
            return obj != null && CanPlayerSee(obj.Pos);
        }

        public bool CanPlayerSee(Pos2D pos)
        {
            return Player != null && Player.CanSee(pos);
        }

        public CommandContext Clone() => new CommandContext(Level, GameService, EntityService, CombatManager, LootProvider);

        public void PreviewObjectHurt(GameObjectBase attacker, GameObjectBase defender, int damage, DamageType damageType)
        {
            OnActorHurt?.Invoke(this, new ActorDamagedEventArgs(attacker, defender, damage, damageType));
        }

        public void SetLevel(LevelData level)
        {
            Level = level;
            Player = Level.FindPlayer();
        }

        public void ReplacePlayer(Player player, Pos2D position)
        {
            RemoveObject(Player);

            player.Pos = position;
            Level.AddObject(player);
            Player = player;

        }

        /// <inheritdoc />
        public void DisplayHelp(GameObjectBase source, string helpTopic)
        {
            var topic = helpTopic.ToLowerInvariant();
            string message = null;

            if (topic.StartsWith("help_actor_"))
            {
                var definition = EntityService.GetEntity(helpTopic.Substring(5));

                if (definition != null && !string.IsNullOrWhiteSpace(definition.HelpText))
                {
                    message = definition.HelpText;
                }
            }
            else
            {
                switch (topic)
                {
                    case "help_firewalls":
                        message =
                            "Exits are protected by a firewall. Capture every core on a machine in order to move on.";
                        break;

                    case "help_welcome":
                        message =
                            "You're an AI inside of a computer network. Travel between systems and escape to the Internet.";
                        break;

                    default:
                        AddMessage($"No help found for topic '{helpTopic}'.", ClientMessageType.Assertion);
                        return;
                }
            }

            if (!string.IsNullOrWhiteSpace(message))
            {
                // If the source of the message is corrupt, randomize the casing
                if (source != null && source.IsCorrupted)
                {
                    var noiseChars = "!@#$%&?,";
                    var sb = new StringBuilder();
                    foreach (var c in message.ToLowerInvariant())
                    {
                        // Ignore spaces
                        if (c == ' ')
                        {
                            sb.Append(c);
                            continue;
                        }

                        var rng = Randomizer.GetInt(0, 2);
                        switch (rng)
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

                    message = sb.ToString();
                }

                AddEffect(new HelpTextEffect(source, message));
            }
        }

        public CombatManager CombatManager { get; }
        public LootProvider LootProvider { get; }

        [CanBeNull]
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
                LevelType.RouterGateway,
                LevelType.Escaped
            };

            var levelType = Level.Id;

            // If we've reached the end of the series of levels, it's time to win.
            int levelIndex = levels.IndexOf(levelType);
            GameService.MoveToLevel(levels[levelIndex + 1], this);
        }

        public void AddError(string message)
        {
            // TODO: Logging this would be good.
            AddMessage(message, ClientMessageType.Assertion);
        }

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

        public void MoveExecutingActor(Pos2D newPos) => MoveObject(Player, newPos);
        public void UpdateObject(GameObjectBase gameObject) => AddMessage(new ObjectUpdatedMessage(gameObject));

        public void CreatedObject(GameObjectBase gameObject) => AddMessage(new CreatedMessage(gameObject));

        public void DisplayText(string text, ClientMessageType messageType = ClientMessageType.Generic) => AddMessage(new DisplayTextMessage(text, messageType));

        /// <inheritdoc />
        public void TeleportActor(Actor actor, Pos2D pos)
        {

            const int damage = 1;

            // Ensure that the teleport is allowed to happen - don't allow teleporting into walls
            var targetCell = Level.GetCell(pos);
            if (targetCell == null || targetCell.HasNonActorObstacle)
            {
                if (actor.IsPlayer || CanPlayerSee(pos))
                {
                    AddMessage($"{actor.Name} tries to teleport but is blocked, causing {damage} scramble damage.",
                               ClientMessageType.Failure);
                }

                CombatManager.HurtObject(this, actor, damage, actor, "scrambles", DamageType.Normal); // Ignoring this is fine
                return;
            }

            // See who else is here
            var telefragged = Level.Actors.Where(a => a.Pos == pos).ToList();
            var oldPos = actor.Pos;

            // Add an effect for the teleportation
            if (CanPlayerSee(actor.Pos) || CanPlayerSee(pos) || actor.IsPlayer)
            {
                // Don't give the client-application an unfair idea of where the target teleported to if they can't see it
                var endPos = pos;
                if (!actor.IsPlayer && !CanPlayerSee(pos))
                {
                    endPos = new Pos2D(-500, -500);
                }

                AddEffect(new TeleportEffect(actor.Pos, endPos));
            }

            // Actually move the actor
            Level.MoveObject(actor, pos);

            // Teleporting on top of another actor should always cause damage to that actor and swap it to your old location
            foreach (var target in telefragged)
            {
                var hurtMessage = CombatManager.HurtObject(this, target, damage, actor, "scrambles", DamageType.Normal);

                if (actor.IsPlayer || target.IsPlayer || CanPlayerSee(pos))
                {
                    AddMessage(hurtMessage, ClientMessageType.Generic);
                }

                Level.MoveObject(target, oldPos);
            }

            // If any collisions occurred, also hurt the actor who triggered it
            if (telefragged.Any())
            {
                var hurtMessage = CombatManager.HurtObject(this, actor, damage, actor, "scrambles", DamageType.Normal);

                if (actor.IsPlayer || Player.CanSee(pos))
                {
                    AddMessage(hurtMessage, ClientMessageType.Generic);
                }
            }

        }

        public void CalculateLineOfSight(Actor actor)
        {
            var fov = new ShadowCasterViewProvider(Level);
            fov.ComputeFov(actor.Pos, actor.EffectiveLineOfSightRadius);

            actor.VisibleCells = fov.VisiblePositions;
            actor.MarkCellsAsKnown(fov.VisiblePositions);
        }

        public void AddSoundEffect(OpenableGameObjectBase source, SoundEffects sound) 
            => AddEffect(new SoundEffect(source, sound));
    }

}