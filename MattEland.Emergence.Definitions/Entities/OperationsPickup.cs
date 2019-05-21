using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class OperationsPickup : GameObjectBase
    {
        public OperationsPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'o';
        protected override string CustomName => "Operations Restore";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                actor.Operations += Potency;
                context.AddEffect(new OpsChangedEffect(this, Potency));
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 5;

        public override int ZIndex => 10;

    }
}