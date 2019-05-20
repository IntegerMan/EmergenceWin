using System;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public abstract class WorldObject
    {
        protected WorldObject(Pos2D pos, Guid id)
        {
            Pos = pos;
            Id = id;
        }

        public Guid Id { get; }

        public Pos2D Pos { get; set; }

        public abstract char AsciiChar { get; }

        public abstract int ZIndex { get; }
        
        public virtual string BackgroundColor => GameColors.Black;
        public virtual string ForegroundColor => GameColors.Gray;

    }
}