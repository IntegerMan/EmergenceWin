using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public class StabilityPickup : GameObjectBase
    {
        public StabilityPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';

        public override string Name => "Stability Restore";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                actor.Stability += Potency;
                context.AddEffect(new StabilityRestoreEffect(this, Potency));
                context.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 5;

        public override int ZIndex => 10;
        
    }
}