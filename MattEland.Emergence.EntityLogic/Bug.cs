using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.EntityLogic
{
    public class Bug : Actor
    {
        public Bug(ActorDto dto) : base(dto)
        {
        }

        public override DamageType AttackDamageType => DamageType.Combination;

        public override void OnWaited(ICommandContext context)
        {
            base.OnWaited(context);

            // Bugs can pretty quickly get corruption to cascade throughout a level, so tone down passive infections
            if ( context.Randomizer.GetInt(0, 2) == 0)
            {
                var cell = context.Level.GetCell(Position);

                if (cell != null)
                {
                    cell.Corruption += 1;
                }
            }
        }

        public override void ApplyCorruptionDamage(ICommandContext context, IGameObject source, int damage)
        {
            if (damage < 0)
            {
                var message = context.CombatManager.HurtObject(context, this, -damage, source, "cleanses", DamageType.Normal);
                if (context.CanPlayerSee(Position))
                {
                    context.AddMessage(message, ClientMessageType.Generic);
                }
            }
        }

        public override bool IsCorruptable => false;
    }
}