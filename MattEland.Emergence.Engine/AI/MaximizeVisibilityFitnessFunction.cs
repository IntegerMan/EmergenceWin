using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class MaximizeVisibilityFitnessFunction : ActorFitnessFunctionBase
    {
        public MaximizeVisibilityFitnessFunction(ICommandContextGenerator contextGenerator,
            IArtificialIntelligenceService aiService,
            string actorId,
            ILoggerFactory logFactory)
            : base(contextGenerator, aiService, actorId, logFactory)
        {
        }

        protected override FitnessTracker ScoreActor(FitnessTracker score,
                                                     ICommandContext context,
                                                     IActor actor)
        {
            return score.AdjustScoreByStability(actor)
                        .AdjustScoreByVisibleNonSystemEntities(actor)
                        .AdjustScoreByProximityToPlayer(actor, 2)
                        .AdjustScoreByActorVisibleTileCount(actor, 5)
                        .AdjustScoreByMovedRemainedStationary(actor);
        }
    }
}