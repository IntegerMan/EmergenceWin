using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.AI
{
    public class ArtificialIntelligenceService
    {
        [NotNull]
        public GameContext Context { get; }

        public CommonBehaviors CommonBehaviors { get; } = new CommonBehaviors();

        public ArtificialIntelligenceService([NotNull] GameContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [NotNull]
        public BehaviorTreeResult ProcessActorTurn([NotNull] Actor actor)
        {
            if (actor == null) throw new ArgumentNullException(nameof(actor));

            var result = new BehaviorTreeResult();

            // Casuals begone
            if (actor.IsPlayer || actor.IsDead) return result;

            // We're going to evaluate only the tiles immediately in front of the actor
            var choices = Context.GetCellsVisibleFromPoint(actor.Pos, actor.EffectiveLineOfSightRadius).ToList();

            // Grab behaviors that are applicable for this actor
            var behaviors = actor.GetBehaviors(Context);

            // Evaluate each behavior in sequence
            foreach (var behavior in behaviors)
            {
                result.EvaluatedBehaviors.Add(behavior);

                // If any behavior marked itself as the handler of the event, do not evaluate future behaviors
                if (behavior.Evaluate(Context, actor, choices))
                {
                    result.SelectedBehavior = behavior;
                    break;
                }
            }

            return result;
        }
    }
}