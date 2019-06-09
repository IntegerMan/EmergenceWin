using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public abstract class VirusActorBase : Actor
    {
        protected VirusActorBase(Pos2D pos) : base(pos)
        {
        }

        public override DamageType AttackDamageType => DamageType.Combination;

        public override void OnWaited(GameContext context)
        {
            base.OnWaited(context);

            // Bugs can pretty quickly get corruption to cascade throughout a level, so tone down passive infections
            if (context.Randomizer.GetInt(0, 2) == 0)
            {
                var cell = context.Level.GetCell(Pos);

                if (cell != null)
                {
                    cell.Corruption += 1;
                }
            }
        }

        public override void ApplyCorruptionDamage(GameContext context, GameObjectBase source, int damage)
        {
            if (damage >= 0) return;

            var message = context.CombatManager.HurtObject(context, source, this, -damage, "cleanses", DamageType.Normal);
            if (context.CanPlayerSee(Pos))
            {
                context.AddMessage(message, ClientMessageType.Generic);
            }
        }

        public override bool IsCorruptable => false;
    }
}