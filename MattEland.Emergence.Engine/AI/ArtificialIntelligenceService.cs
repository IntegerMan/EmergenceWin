using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.AI
{
    public class ArtificialIntelligenceService
    {
        [NotNull]
        public GameContext Context { get; }

        public ArtificialIntelligenceService([NotNull] GameContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void ProcessActorTurn([NotNull] Actor actor)
        {
            if (actor == null) throw new ArgumentNullException(nameof(actor));

            // Casuals begone
            if (actor.IsPlayer || actor.IsDead) return;

            // We're going to evaluate only the tiles immediately in front of the actor
            var choices = Context.GetCellsVisibleFromPoint(actor.Pos, actor.EffectiveLineOfSightRadius).ToList();

            // Grab behaviors that are applicable for this actor
            var behaviors = GetBehaviors(actor);

            // Evaluate each behavior in sequence
            foreach (var behavior in behaviors)
            {
                // If any behavior marked itself as the handler of the event, do not evaluate future behaviors
                if (behavior.Evaluate(Context, actor, choices))
                {
                    break;
                }
            }
        }


        [NotNull, ItemNotNull]
        private IEnumerable<ActorBehaviorBase> GetBehaviors([NotNull] Actor actor)
        {
            if (!actor.IsImmobile)
            {
                yield return new WanderBehavior();
            }

            yield return new IdleBehavior();
        }
    }
}