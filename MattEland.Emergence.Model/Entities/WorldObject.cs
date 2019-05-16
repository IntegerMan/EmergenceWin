using System;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public abstract class WorldObject
    {
        protected WorldObject(Position pos, Guid id)
        {
            Pos = pos;
            Id = id;
        }

        public Guid Id { get; }

        public Position Pos { get; set; }

        public abstract char AsciiChar { get; }

        public abstract int ZIndex { get; }
        
        public virtual string BackgroundColor => "#FF000000";
        public virtual string ForegroundColor => "#FF999999";

    }
}