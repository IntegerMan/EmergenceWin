using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Items
{
    public class StabilityPickup : GameObjectBase
    {
        public StabilityPickup(Pos2D pos) : base(pos)
        {
        }

        public override char AsciiChar => 'p';

        public override string Name => "Stability Restore";

        public override GameObjectType ObjectType => GameObjectType.GenericPickup;

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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