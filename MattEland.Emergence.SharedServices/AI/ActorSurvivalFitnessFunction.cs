﻿using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using Microsoft.Extensions.Logging;

namespace MattEland.Emergence.Services.AI
{
    public class ActorSurvivalFitnessFunction : ActorFitnessFunctionBase
    {
        public ActorSurvivalFitnessFunction(ICommandContextGenerator contextGenerator,
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
            return score.AdjustScoreByStability(actor, 1000)
                        .AdjustScoreByMovedRemainedStationary(actor, 50);
        }
    }
}