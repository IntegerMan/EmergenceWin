using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities
{
    public class Worm : Bug
    {
        public Worm(ActorDto dto) : base(dto)
        {
        }

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