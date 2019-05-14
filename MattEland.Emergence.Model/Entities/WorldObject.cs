using System;

namespace MattEland.Emergence.Model.Entities
{
    public abstract class WorldObject
    {
        protected WorldObject(Position pos, Guid id)
        {
            Pos = pos;
            Id = id;
        }

        public Guid Id { get; set; }

        public Position Pos { get; set; }

        public abstract char AsciiChar { get; }

        public abstract int ZIndex { get; }
    }
}