using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Debris : GameObjectBase
    {
        public Debris(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;
        public override bool IsInteractive => true;
        public override char AsciiChar => '_';
        public override bool IsCorruptable => false;

        public override string Name => "Metadata";

        public override string ForegroundColor => GameColors.Brown;
    }
}