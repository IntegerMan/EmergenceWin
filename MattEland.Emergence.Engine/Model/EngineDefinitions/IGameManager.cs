using System.Collections.Generic;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model.Messages;

namespace MattEland.Emergence.Engine.Model.EngineDefinitions
{
    public interface IGameManager
    {
        GameStatus State { get; }
        IPlayer Player { get; }
        IEnumerable<GameMessage> Start();
        IEnumerable<GameMessage> GenerateLevel();
        IEnumerable<GameMessage> MovePlayer(MoveDirection direction);
    }
}