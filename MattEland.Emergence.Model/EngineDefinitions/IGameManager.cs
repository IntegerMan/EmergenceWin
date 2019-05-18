using System.Collections.Generic;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Entities;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Engine
{
    public interface IGameManager
    {
        GameState State { get; }
        Actor Player { get; }
        IEnumerable<GameMessage> Start();
        IEnumerable<GameMessage> GenerateLevel();
        IEnumerable<GameMessage> MovePlayer(MoveDirection direction);
    }
}