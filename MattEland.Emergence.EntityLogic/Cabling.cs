using MattEland.Emergence.Definitions.DTOs;

namespace MattEland.Emergence.EntityLogic
{
    public class Cabling : GameObjectBase
    {
        public Cabling(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true; // For now
        public override bool IsTargetable => false; // For now

        protected override string CustomName => "Cabling";
    }
}