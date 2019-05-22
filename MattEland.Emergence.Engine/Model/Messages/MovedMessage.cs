using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Model.Messages
{
    public class MovedMessage : GameMessage
    {
        [NotNull] 
        public GameObjectBase Source { get; }
        
        public Pos2D OldPos { get; }
        public Pos2D NewPos { get; }

        public MovedMessage([NotNull] GameObjectBase source, Pos2D oldPos, Pos2D newPos)
        {
            Source = source;
            OldPos = oldPos;
            NewPos = newPos;
        }

        public override string ToString() => $"Move {Source.ObjectId.ToString().Substring(0, 5)}... from {OldPos.ToString()} to {NewPos.ToString()}";
    }
}