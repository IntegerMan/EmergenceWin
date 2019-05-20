using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class MovedMessage : GameMessage
    {
        [NotNull] 
        public IGameObject Source { get; }
        
        public Pos2D OldPos { get; }
        public Pos2D NewPos { get; }

        public MovedMessage([NotNull] IGameObject source, Pos2D oldPos, Pos2D newPos)
        {
            Source = source;
            OldPos = oldPos;
            NewPos = newPos;
        }

        public override string ToString() => $"Move {Source.ObjectId.ToString().Substring(0, 5)}... from {OldPos.ToString()} to {NewPos.ToString()}";
    }
}