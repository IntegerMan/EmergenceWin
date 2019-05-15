using JetBrains.Annotations;
using MattEland.Emergence.Model.Entities;

namespace MattEland.Emergence.Model.Messages
{
    public class MovedMessage : GameMessage
    {
        [NotNull] 
        public WorldObject Source { get; }
        
        public Position OldPos { get; }
        public Position NewPos { get; }

        public MovedMessage([NotNull] WorldObject source, Position oldPos, Position newPos)
        {
            Source = source;
            OldPos = oldPos;
            NewPos = newPos;
        }

        public override string ToString() => $"Move {Source.Id.ToString().Substring(0, 5)}... from {OldPos.ToString()} to {NewPos.ToString()}";
    }
}