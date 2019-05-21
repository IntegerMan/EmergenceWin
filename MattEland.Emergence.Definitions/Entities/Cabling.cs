using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Cabling : GameObjectBase
    {
        public Cabling(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true; // For now
        public override bool IsTargetable => false; // For now

        protected override string CustomName => "Cabling";
        public override char AsciiChar => '-';
        public override string ForegroundColor => GameColors.LightGray;
    }
}