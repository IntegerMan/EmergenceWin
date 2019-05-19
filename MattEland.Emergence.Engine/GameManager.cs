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
using ActorType = MattEland.Emergence.Model.Entities.ActorType;

namespace MattEland.Emergence.Engine
{
    public class GameManager : IGameManager
    {
        public GameManager()
        {
            _levelBuilder = new LevelGenerationService(new PrefabService(), new EncountersService(), new BasicRandomization());
        }

        [CanBeNull]
        private Actor _player;

        private IList<WorldObject> _objects;

        private int _nextLevelId;

        [NotNull]
        private readonly LevelGenerationService _levelBuilder;

        public GameState State { get; private set; }
        public Actor Player => _player;

        public IEnumerable<GameMessage> Start()
        {
            // TODO: Should this be static and create a new instance of a GameManager instead?

            if (State != GameState.NotStarted) throw new InvalidOperationException("The game has already been started");

            _nextLevelId = 0;
            
            return GenerateLevel();
        }

        public IEnumerable<GameMessage> GenerateLevel()
        {
            State = GameState.Executing;

            var level = _levelBuilder.GenerateLevel(new LevelGenerationParameters()
            {
                LevelType = LevelType.Tutorial,
                PlayerId = "ACTOR_PLAYER_GAME"

            }, null); //_player);

            var map = WorldGenerator.generateMap(_nextLevelId++, _player);
            
            _objects = map.Objects.ToList();

            State = GameState.Ready;

            _player = _objects.OfType<Actor>().Single(a => a.ActorType == ActorType.Player);
            _player.Pos = map.PlayerStart;
            
            return _objects.Map(o => new CreatedMessage(o));
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
