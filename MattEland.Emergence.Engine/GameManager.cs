using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Entities;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using MattEland.Emergence.Definitions.Model.Messages;
using MattEland.Emergence.LevelGeneration;
using MattEland.Emergence.LevelGeneration.Encounters;
using MattEland.Emergence.LevelGeneration.Prefabs;
using MattEland.Shared.Collections;

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

            var level = _levelBuilder.GenerateLevel(new LevelGenerationParameters()
            {
                LevelType = _levels.Dequeue(),
                PlayerId = PlayerId

            }, _player);

            // Ready for new objects
            _objects.Clear();

            // Add the player
            _player.Pos = level.PlayerStart;
            _objects.Add(_player);

            // Add all objects
            level.Cells.Each(c => GetGameObjectForCell(c).Each(o => _objects.Add(o)));

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
                yield return new Floor(new GameObjectDto()
                {
                    Pos = c.Pos.SerializedValue
                }, c.FloorType);
            }
        }

/*        private static WorldObject GetGameObjectFromOldObject(IGameObject gameObject)
        {
            var pos = gameObject.Pos;

            switch (gameObject.ObjectType)
            {
                case GameObjectType.Core: return new Core(pos);
                case GameObjectType.Divider: return new Obstacle(pos, ObstacleType.Barrier);
                case GameObjectType.Cabling: return new Floor(pos, FloorType.DecorativeTile); // TODO: No
                case GameObjectType.Turret: return new Obstacle(pos, ObstacleType.Service); // TODO: No
                case GameObjectType.Firewall: return new Firewall(pos);
                case GameObjectType.Exit: return new Stairs(pos, true);
                case GameObjectType.Entrance: return new Stairs(pos, false);
                case GameObjectType.Service: return new Obstacle(pos, ObstacleType.Service);
                case GameObjectType.DataStore: return new Obstacle(pos, ObstacleType.Data);
                case GameObjectType.Wall: return new Obstacle(pos, ObstacleType.Wall);
                case GameObjectType.Debris: return new Floor(pos, FloorType.DecorativeTile); // TODO: No
                case GameObjectType.Door: return new Door(pos);
                case GameObjectType.CommandPickup: return new HelpTile(pos, "Command Pickup");
                case GameObjectType.Treasure: return new HelpTile(pos, "Treasure");
                case GameObjectType.Water: return new Obstacle(pos, ObstacleType.ThreadPool);
                case GameObjectType.Actor: return new Actor(pos, ((Definitions.Entities.Actor) gameObject).ActorType); // TODO: ActorType
                case GameObjectType.Player: return new Actor(pos, ActorType.Player);
                case GameObjectType.Help: return new HelpTile(pos, "Help");
                case GameObjectType.CharacterSelect: return new CharacterSelect(pos);
                case GameObjectType.GenericPickup: return new HelpTile(pos, "Generic Pickup");
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }*/

        public IEnumerable<GameMessage> MovePlayer(MoveDirection direction)
        {
            if (State != GameStatus.Ready) throw new InvalidOperationException("The game is not ready for input");

            State = GameStatus.Executing;

            var targetPos = _player.Pos.GetNeighbor(direction);

            var context = new CommandContext(this, _player, _objects);

            foreach (var obj in _objects.Where(o => o.Pos == targetPos).OrderByDescending(o => o.ZIndex))
            {
                // TODO: Some messages should stop future interactions
                obj.OnInteract(context, _player);
            }

            State = GameStatus.Ready;

            return context.Messages;
        }
    }


}
