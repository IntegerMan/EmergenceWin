using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using Microsoft.Extensions.Logging;

namespace MattEland.Emergence.Services.AI
{
    public class AnnoyPlayerFitnessFunction : ActorFitnessFunctionBase
    {
        public AnnoyPlayerFitnessFunction(ICommandContextGenerator contextGenerator,
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
                        .AdjustScoreByProximityToPlayer(actor, 25)
                        .AdjustScoreByActorVisibleTileCount(actor, 2)
                        .AdjustScoreByMovedRemainedStationary(actor);
        }
    }
}