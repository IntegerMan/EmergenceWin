using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Model.EngineDefinitions;
using MattEland.Emergence.Engine.Model.Messages;

namespace MattEland.Emergence.Engine
{
    public class GameManager
    {
        public GameManager()
        {
            _service = new GameService();
        }

        [NotNull]
        private readonly GameService _service;

        public GameStatus State { get; private set; }

        public Player Player => _service.Player;

        public IEnumerable<GameMessage> Start()
        {
            if (State != GameStatus.NotStarted) throw new InvalidOperationException("The game has already been started");

            var context = _service.StartNewGame();

            State = GameStatus.Ready;

            return context.Messages;
        }

        public IEnumerable<GameMessage> MovePlayer(MoveDirection direction)
        {
            if (State != GameStatus.Ready) throw new InvalidOperationException("The game is not ready for input");

            State = GameStatus.Executing;

            var context = _service.HandleGameMove(direction);

            State = GameStatus.Ready;

            return context.Messages;
        }
    }


}
