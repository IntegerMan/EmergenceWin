using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Entities;
using MattEland.Emergence.Definitions.Model.Messages;

namespace MattEland.Emergence.Definitions.Model.EngineDefinitions
{
    public interface ICommandContext
    {
        IGameManager GameManager { get; }
        
        Actor Actor { get; }
        void MoveObject(WorldObject gameObject, Pos2D newPos);
        void MoveExecutingActor(Pos2D newPos);
        void UpdateObject(WorldObject gameObject);
        void UpdateCapturedCores();
        void DisplayText(string text);

        IEnumerable<GameMessage> Messages { get; }
        void AdvanceToNextLevel();
    }
}