using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class Debris : WalkableObject
    {
        public Debris(Pos2D pos) : base(pos)
        {
        }

        public override GameObjectType ObjectType => GameObjectType.Debris;
        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;
        public override char AsciiChar => '_';
        public override bool IsCorruptable => false;

        public override string Name => "Metadata";

        public override string ForegroundColor => GameColors.Brown;
    }
}