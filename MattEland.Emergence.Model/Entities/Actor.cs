using System;

namespace MattEland.Emergence.Model.Entities
{
    public class Actor : WorldObject
    {
        public Actor(Position pos, ActorType entityType) : base(pos, Guid.NewGuid())
        {
            ActorType = entityType;
        }

        public override string ForegroundColor => GameColors.Green;

        public override char AsciiChar => '@';
        public override int ZIndex => 90;
        public ActorType ActorType { get; }
    }
}