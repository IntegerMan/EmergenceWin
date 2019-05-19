using System.Linq;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.EntityLogic
{
    public class Worm : Bug
    {
        public Worm(ActorDto dto) : base(dto)
        {
        }

        public override DamageType AttackDamageType => DamageType.Corruption;

        public override void ApplyActiveEffects(ICommandContext context)
        {
            base.ApplyActiveEffects(context);

            var cell = context.Level.GetCell(Position);

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