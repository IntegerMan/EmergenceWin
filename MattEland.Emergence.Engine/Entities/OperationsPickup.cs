using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public class OperationsPickup : GameObjectBase
    {
        public OperationsPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'o';
        public override string Name => "Operations Restore";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                actor.Operations += Potency;
                context.AddEffect(new OpsChangedEffect(this, Potency));
                context.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 5;

        public override int ZIndex => 10;

    }
}