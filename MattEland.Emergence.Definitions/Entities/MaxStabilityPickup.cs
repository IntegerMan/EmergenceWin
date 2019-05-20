using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class MaxStabilityPickup : GameObjectBase
    {
        public MaxStabilityPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';
        protected override string CustomName => $"Max Stability +{Potency}";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                actor.MaxStability += Potency;
                actor.Stability += Potency;

                context.AddEffect(new HelpTextEffect(this, $"Max Stability +{Potency}"));
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 1;

        public override int ZIndex => 10;
    }
}