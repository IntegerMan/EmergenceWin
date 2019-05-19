using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.LevelGeneration;
using MattEland.Emergence.LevelGeneration.Encounters;
using MattEland.Emergence.LevelGeneration.Prefabs;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;
using MattEland.Emergence.Model.Messages;
using MattEland.Shared.Collections;
using ActorType = MattEland.Emergence.Model.Entities.ActorType;
using FloorType = MattEland.Emergence.Model.Entities.FloorType;

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
        private Actor _player;

        private readonly List<WorldObject> _objects = new List<WorldObject>();

        [NotNull]
        private readonly LevelGenerationService _levelBuilder;

        private readonly Queue<LevelType> _levels;

        public GameState State { get; private set; }
        public Actor Player => _player;

        public IEnumerable<GameMessage> Start()
        {
            // TODO: Should this be static and create a new instance of a GameManager instead?

            if (State != GameState.NotStarted) throw new InvalidOperationException("The game has already been started");

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
            var player = _player;
            if (_player == null)
            {
                _player = new Actor(new Position(0, 0), ActorType.Player);
            }

            State = GameState.Executing;

            var level = _levelBuilder.GenerateLevel(new LevelGenerationParameters()
            {
                LevelType = _levels.Dequeue(),
                PlayerId = "ACTOR_PLAYER_GAME"

            }, null);

            // Ready for new objects
            _objects.Clear();

            // Add the player
            _player.Pos = new Position(level.PlayerStart.X, level.PlayerStart.Y);
            _objects.Add(_player);

            // Add all objects
            level.Cells.Each(c => GetGameObjectForCell(c).Each(o => _objects.Add(o)));

            State = GameState.Ready;

            return _objects.Select(o => new CreatedMessage(o));
        }

        private static IEnumerable<WorldObject> GetGameObjectForCell(IGameCell c)
        {

            bool hasObject = false;
            foreach (var gameObject in c.Objects)
            {
                yield return GetGameObjectFromOldObject(gameObject);
                hasObject = true;
            }

            if (c.FloorType != Definitions.Level.FloorType.Void && !hasObject)
            {
                yield return new Floor(new Position(c.Pos.X, c.Pos.Y), GetFloorType(c.FloorType));
            }
        }

        private static FloorType GetFloorType(Definitions.Level.FloorType legacyFloorType)
        {
            switch (legacyFloorType)
            {
                case Definitions.Level.FloorType.DecorativeTile:
                    return FloorType.QuadTile;
                case Definitions.Level.FloorType.Walkway:
                    return FloorType.Grate;
                case Definitions.Level.FloorType.CautionMarker:
                    return FloorType.Caution;
                case Definitions.Level.FloorType.Normal:
                default:
                    return FloorType.LargeTile;
            }
        }

        private static WorldObject GetGameObjectFromOldObject(IGameObject gameObject)
        {
            var pos = new Position(gameObject.Position.X, gameObject.Position.Y);

            switch (gameObject.ObjectType)
            {
                case GameObjectType.Core: return new Core(pos);
                case GameObjectType.Divider: return new Obstacle(pos, ObstacleType.Barrier);
                case GameObjectType.Cabling: return new Floor(pos, FloorType.Grate); // TODO: No
                case GameObjectType.Turret: return new Obstacle(pos, ObstacleType.Service); // TODO: No
                case GameObjectType.Firewall: return new Firewall(pos);
                case GameObjectType.Exit: return new Stairs(pos, true);
                case GameObjectType.Entrance: return new Stairs(pos, false);
                case GameObjectType.Service: return new Obstacle(pos, ObstacleType.Service);
                case GameObjectType.DataStore: return new Obstacle(pos, ObstacleType.Data);
                case GameObjectType.Wall: return new Obstacle(pos, ObstacleType.Wall);
                case GameObjectType.Debris: return new Floor(pos, FloorType.LargeTile); // TODO: No
                case GameObjectType.Door: return new Door(pos);
                case GameObjectType.CommandPickup: return new HelpTile(pos, "Command Pickup");
                case GameObjectType.Treasure: return new HelpTile(pos, "Treasure");
                case GameObjectType.Water: return new Obstacle(pos, ObstacleType.ThreadPool);
                case GameObjectType.Actor: return new Actor(pos, ActorType.Player); // TODO: ActorType
                case GameObjectType.Player: return new Actor(pos, ActorType.Player);
                case GameObjectType.Help: return new HelpTile(pos, "Help");
                case GameObjectType.CharacterSelect: return new CharacterSelect(pos);
                case GameObjectType.GenericPickup: return new HelpTile(pos, "Generic Pickup");
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public IEnumerable<GameMessage> MovePlayer(MoveDirection direction)
        {
            if (State != GameState.Ready) throw new InvalidOperationException("The game is not ready for input");

            State = GameState.Executing;

            var targetPos = _player.Pos.GetNeighbor(direction);

            var context = new CommandContext(this, _player, _objects);

            foreach (var obj in _objects.Where(o => o.Pos == targetPos).OrderByDescending(o => o.ZIndex).OfType<IInteractive>())
            {
                // TODO: Some messages should stop future interactions
                obj.Interact(context);
            }

            State = GameState.Ready;

            return context.Messages;
        }
    }


}
