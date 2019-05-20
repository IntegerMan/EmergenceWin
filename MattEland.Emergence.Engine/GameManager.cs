using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.AI;
using MattEland.Emergence.AI.Brains;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Entities;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using MattEland.Emergence.Definitions.Model.Messages;
using MattEland.Emergence.LevelGeneration;
using MattEland.Emergence.LevelGeneration.Encounters;
using MattEland.Emergence.LevelGeneration.Prefabs;
using MattEland.Emergence.Services.Game;
using MattEland.Shared.Collections;
using CommandContext = MattEland.Emergence.Definitions.Model.EngineDefinitions.CommandContext;

namespace MattEland.Emergence.Engine
{
    public class GameManager : IGameManager
    {
        public GameManager()
        {
            _levelBuilder = new LevelGenerationService(new PrefabService(), new EncountersService(), new BasicRandomization());

            _levels = new Queue<LevelType>();
        }

        [CanBeNull]
        private Player _player;

        private readonly List<IGameObject> _objects = new List<IGameObject>();

        [NotNull]
        private readonly LevelGenerationService _levelBuilder;

        private readonly Queue<LevelType> _levels;
        private ILevel _level;

        public GameStatus State { get; private set; }
        public Player Player => _player;

        public IEnumerable<GameMessage> Start()
        {
            // TODO: Should this be static and create a new instance of a GameManager instead?

            if (State != GameStatus.NotStarted) throw new InvalidOperationException("The game has already been started");

            _levels.Clear();
            _levels.Enqueue(LevelType.Tutorial);
            _levels.Enqueue(LevelType.ClientWorkstation);
            _levels.Enqueue(LevelType.SmartFridge);
            _levels.Enqueue(LevelType.MessagingServer);
            _levels.Enqueue(LevelType.Bastion);
            _levels.Enqueue(LevelType.RouterGateway);
            _levels.Enqueue(LevelType.Escaped);

            return GenerateLevel();
        }

        public IEnumerable<GameMessage> GenerateLevel()
        {
            State = GameStatus.Executing;

            const string PlayerId = "ACTOR_PLAYER_GAME";

            if (_player == null)
            {
                _player = new Player(new PlayerDto { ObjectId = PlayerId});
            }

            _level = _levelBuilder.GenerateLevel(new LevelGenerationParameters
            {
                LevelType = _levels.Dequeue(),
                PlayerId = PlayerId

            }, _player);

            // Ready for new objects
            _objects.Clear();

            // Add the player
            _player.Pos = _level.PlayerStart;
            _objects.Add(_player);

            // Add all objects
            _level.Cells.Each(c => GetGameObjectForCell(c).Each(o => _objects.Add(o)));

            State = GameStatus.Ready;

            return _objects.Select(o => new CreatedMessage(o));
        }

        private static IEnumerable<IGameObject> GetGameObjectForCell(IGameCell c)
        {
            bool hasObject = false;
            foreach (var gameObject in c.Objects)
            {
                yield return gameObject;

                if (!(gameObject is Actor))
                {
                    hasObject = true;
                }
            }

            if (c.FloorType != FloorType.Void && !hasObject)
            {
                yield return new Floor(new GameObjectDto
                {
                    Pos = c.Pos.SerializedValue
                }, c.FloorType);
            }
        }

        public IEnumerable<ClientMessage> MovePlayer(MoveDirection direction)
        {
            if (State != GameStatus.Ready) throw new InvalidOperationException("The game is not ready for input");

            State = GameStatus.Executing;

            var targetPos = _player.Pos.GetNeighbor(direction);

            var aiService = new ArtificialIntelligenceService(new LegacyBrainProvider());
            var entityService = new EntityDefinitionService();
            var combatManager = new CombatManager();
            var randomization = new BasicRandomization();

            var service = new GameService(_levelBuilder, aiService, combatManager, null, entityService, new GameSimulationManager(),  randomization );
            MattEland.Emergence.Definitions.Services.ICommandContext context = new MattEland.Emergence.Services.Game.CommandContext(_level, service, entityService, combatManager, null, randomization );

            //var context = new CommandContext(this, _player, _objects);

            foreach (var obj in _objects.Where(o => o.Pos == targetPos).OrderByDescending(o => o.ZIndex))
            {
                // Some messages should stop future interactions
                if (!obj.OnActorAttemptedEnter(context, _player))
                {
                    break;
                }
            }

            State = GameStatus.Ready;

            return context.Messages;
        }
    }


}
