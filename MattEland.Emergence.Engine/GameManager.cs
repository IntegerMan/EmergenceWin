using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Engine
{
    public class GameManager
    {
        [CanBeNull]
        private Actor _player;

        private IList<WorldObject> _objects;

        public GameState State { get; private set; }
        public Actor Player => _player;

        public IEnumerable<GameMessage> Start()
        {
            // TODO: Should this be static and create a new instance of a GameManager instead?

            if (State != GameState.NotStarted) throw new InvalidOperationException("The game has already been started");

            State = GameState.Executing;

            _objects = WorldGenerator.generateMap(0).ToList();

            State = GameState.Ready;

            _player = _objects.OfType<Actor>().Single(a => a.ActorType == ActorType.Player);

            return _objects.Map(o => new CreatedMessage(o));
        }

        public IEnumerable<GameMessage> MovePlayer(MoveDirection direction)
        {
            if (State != GameState.Ready) throw new InvalidOperationException("The game is not ready for input");

            State = GameState.Executing;

            var targetPos = _player.Pos.GetNeighbor(direction);

            foreach (var obj in _objects.Where(o => o.Pos == targetPos).OrderByDescending(o => o.ZIndex).OfType<IInteractive>())
            {
                // TODO: Some messages should stop future interactions
                foreach (var message in obj.Interact(_player))
                {
                    yield return message;
                }
            }

            State = GameState.Ready;
        }
    }


}
