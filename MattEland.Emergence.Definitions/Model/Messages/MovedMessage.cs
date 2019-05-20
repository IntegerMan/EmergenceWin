using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.Entities;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class MovedMessage : GameMessage
    {
        [NotNull] 
        public WorldObject Source { get; }
        
        public Pos2D OldPos { get; }
        public Pos2D NewPos { get; }

        public MovedMessage([NotNull] WorldObject source, Pos2D oldPos, Pos2D newPos)
        {
            Source = source;
            OldPos = oldPos;
            NewPos = newPos;
        }

        public override string ToString() => $"Move {Source.Id.ToString().Substring(0, 5)}... from {OldPos.ToString()} to {NewPos.ToString()}";
    }
}