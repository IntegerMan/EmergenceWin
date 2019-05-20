using System;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Actor : WorldObject
    {
        public Actor(Pos2D pos, ActorType entityType) : base(pos, Guid.NewGuid())
        {
            ActorType = entityType;
        }

        public override string ForegroundColor => GameColors.Green;

        public override char AsciiChar => '@';
        public override int ZIndex => 90;
        public ActorType ActorType { get; }
    }
}