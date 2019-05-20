using System.Collections.Generic;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Entities;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Messages;

namespace MattEland.Emergence.Definitions.Model.EngineDefinitions
{
    public interface ICommandContext
    {
        IGameManager GameManager { get; }
        
        Player Player { get; }
        void MoveObject(IGameObject gameObject, Pos2D newPos);
        void MoveExecutingActor(Pos2D newPos);
        void UpdateObject(IGameObject gameObject);
        void UpdateCapturedCores();
        void DisplayText(string text, ClientMessageType messageType);

        IEnumerable<GameMessage> Messages { get; }
        void AdvanceToNextLevel();
    }
}