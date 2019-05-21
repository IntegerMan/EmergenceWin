using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class MaxOperationsPickup : GameObjectBase
    {
        public MaxOperationsPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';
        protected override string CustomName => $"Max Operations +{Potency}";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
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

        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayNotImplemented();
        }

    }
}