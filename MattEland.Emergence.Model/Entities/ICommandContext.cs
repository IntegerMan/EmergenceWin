using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public interface ICommandContext
    {
        Actor Actor { get; }
        void MoveObject(WorldObject gameObject, Position newPos);
        void MoveExecutingActor(Position newPos);
        void UpdateObject(WorldObject gameObject);
        void UpdateCapturedCores();
        void DisplayText(string text);

        IEnumerable<GameMessage> Messages { get; }
    }
}