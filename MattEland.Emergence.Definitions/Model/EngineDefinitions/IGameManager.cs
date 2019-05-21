using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Messages;

namespace MattEland.Emergence.Definitions.Model.EngineDefinitions
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