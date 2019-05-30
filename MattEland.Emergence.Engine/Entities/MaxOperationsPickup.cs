using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public class MaxOperationsPickup : GameObjectBase
    {
        public MaxOperationsPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';
        public override string Name => $"Max Operations +{Potency}";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                actor.MaxOperations += Potency;
                actor.Operations += Potency;

                context.AddEffect(new HelpTextEffect(this, $"Max Operations +{Potency}"));
                context.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 1;

        public override int ZIndex => 10;

    }
}