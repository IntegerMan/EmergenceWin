using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class StabilityPickup : GameObjectBase
    {
        public StabilityPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;

        protected override string CustomName => "Stability Restore";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                actor.Stability += Potency;
                context.AddEffect(new StabilityRestoreEffect(this, Potency));
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 5;

        public override int ZIndex => 10;
    }
}