using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
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