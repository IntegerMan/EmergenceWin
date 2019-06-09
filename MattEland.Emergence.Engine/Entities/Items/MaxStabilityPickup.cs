using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Items
{
    public class MaxStabilityPickup : GameObjectBase
    {
        public MaxStabilityPickup(Pos2D pos) : base(pos)
        {
        }

        public override char AsciiChar => 'p';
        public override string Name => $"Max Stability +{Potency}";

        public override GameObjectType ObjectType => GameObjectType.GenericPickup;

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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