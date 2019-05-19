using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.EntityLogic
{
    public class MaxOperationsPickup : GameObjectBase
    {
        public MaxOperationsPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        protected override string CustomName => $"Max Operations +{Potency}";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                actor.MaxOperations += Potency;
                actor.Operations += Potency;

                context.AddEffect(new HelpTextEffect(this, $"Max Operations +{Potency}"));
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 1;

        public override int ZIndex => 10;
    }
}