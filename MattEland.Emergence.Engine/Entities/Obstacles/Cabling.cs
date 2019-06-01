using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class Cabling : WalkableObject
    {
        public Cabling(Pos2D pos) : base(pos)
        {
        }

        public override GameObjectType ObjectType => GameObjectType.Cabling;

        public override bool IsInvulnerable => true; // For now
        public override bool IsTargetable => false; // For now

        public override int ZIndex => 1;

        public override string Name => "Cabling";
        public override char AsciiChar => '-';
        public override string ForegroundColor => GameColors.LightGray;
    }
}