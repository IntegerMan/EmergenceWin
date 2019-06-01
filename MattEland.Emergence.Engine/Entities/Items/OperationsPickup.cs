using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Items
{
    public class OperationsPickup : GameObjectBase
    {
        public OperationsPickup(Pos2D pos) : base(pos)
        {
        }

        public override char AsciiChar => 'o';
        public override string Name => "Operations Restore";

        public override GameObjectType ObjectType => GameObjectType.GenericPickup;

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