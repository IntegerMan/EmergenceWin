﻿using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class CleanseCorruptionFitnessFunction : ActorFitnessFunctionBase
    {

        public CleanseCorruptionFitnessFunction(ICommandContextGenerator contextGenerator,
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
                        .AdjustScoreByDamageGivenReceived(actor)
                        .AdjustScoreByMovedRemainedStationary(actor)
                        .AdjustScoreByCoresControlled(actor)
                        .AdjustScoreByCorruptObjects(-5)
                        .AdjustScoreByCorruptArea(-10);
        }

    }
}