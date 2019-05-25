using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public class MaxStabilityPickup : GameObjectBase
    {
        public MaxStabilityPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';
        public override string Name => $"Max Stability +{Potency}";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                actor.MaxStability += Potency;
                actor.Stability += Potency;

                context.AddEffect(new HelpTextEffect(this, $"Max Stability +{Potency}"));
                context.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 1;

        public override int ZIndex => 10;

    }
}