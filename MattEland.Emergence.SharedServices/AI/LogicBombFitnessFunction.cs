using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using Microsoft.Extensions.Logging;

namespace MattEland.Emergence.Services.AI
{
    public class LogicBombFitnessFunction : ActorFitnessFunctionBase
    {
        public LogicBombFitnessFunction(ICommandContextGenerator contextGenerator,
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
            return score.AdjustScoreByKamakaziFactor(actor)
                        .AdjustScoreByActorVisibleTileCount(actor, 5)
                        .AdjustScoreByProximityToPlayer(actor, 50)
                        .AdjustScoreByMovedRemainedStationary(actor, 20);
        }
    }
}