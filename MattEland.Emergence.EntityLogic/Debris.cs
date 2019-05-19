using MattEland.Emergence.Definitions.DTOs;

namespace MattEland.Emergence.EntityLogic
{
    public class Debris : GameObjectBase
    {
        public Debris(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;
        public override bool IsInteractive => true;
        public override bool IsCorruptable => false;

        protected override string CustomName => "Metadata";
    }
}