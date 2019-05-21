using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
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