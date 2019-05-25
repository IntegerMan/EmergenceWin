using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Cabling : GameObjectBase
    {
        public Cabling(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true; // For now
        public override bool IsTargetable => false; // For now

        public override string Name => "Cabling";
        public override char AsciiChar => '-';
        public override string ForegroundColor => GameColors.LightGray;
    }
}