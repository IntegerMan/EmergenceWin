using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Messages
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

        public override string ToString() => $"Move {Source.Name} from {OldPos.ToString()} to {NewPos.ToString()}";
        
        public override string ForegroundColor => GameColors.Gray;
    }
}