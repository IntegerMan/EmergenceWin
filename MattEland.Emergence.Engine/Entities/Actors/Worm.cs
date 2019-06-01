using System.Linq;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Worm : Bug
    {
        public Worm(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Worm";
        public override char AsciiChar => 'w';

        public override DamageType AttackDamageType => DamageType.Corruption;

        public override void ApplyActiveEffects(GameContext context)
        {
            base.ApplyActiveEffects(context);

            var cell = context.Level.GetCell(Pos);

            if (cell != null)
            {
                cell.Corruption += 1;
                foreach (var cellObject in cell.Objects.Where(o => o.IsCorruptable))
                {
                    cellObject.ApplyCorruptionDamage(context, this, 1);
                }
            }
        }

        public override bool IsCorruptable => false;
    }
}