using System.Collections.Generic;
using MattEland.Emergence.Definitions.Entities;
using MattEland.Emergence.Definitions.Model.Messages;

namespace MattEland.Emergence.Definitions.Model.EngineDefinitions
{
    public interface IGameManager
    {
        GameStatus State { get; }
        Player Player { get; }
        IEnumerable<GameMessage> Start();
        IEnumerable<GameMessage> GenerateLevel();
        IEnumerable<GameMessage> MovePlayer(MoveDirection direction);
    }
}