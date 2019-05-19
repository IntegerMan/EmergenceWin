using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using Microsoft.Extensions.Logging;
using MattEland.Emergence.AI;
using MattEland.Emergence.AI.Sensory;

namespace MattEland.Emergence.Services.AI
{
    public class DefendAlignmentFitnessFunction : ActorFitnessFunctionBase {
        private readonly Alignment _team;

        public DefendAlignmentFitnessFunction(ICommandContextGenerator contextGenerator,
            IArtificialIntelligenceService aiService,
            string actorId,
            ILoggerFactory logFactory,
                                              Alignment team)
            : base(contextGenerator, aiService, actorId, logFactory)
        {
            _team = team;
        }

        protected override FitnessTracker ScoreActor(FitnessTracker score,
                                                     ICommandContext context,
                                                     IActor actor)
        {
            return score.AdjustScoreByStability(actor)
                        .AdjustScoreByDamageGivenReceived(actor)
                        .AdjustScoreByProximityToPlayer(actor, 2)
                        .AdjustScoreByCoresControlled(actor)
                        .AdjustScoreByMovedRemainedStationary(actor);
        }

        protected override void OnActorHurt(FitnessTracker score,
                                            IGameObject attacker,
                                            IGameObject defender,
                                            decimal damage,
                                            DamageType damageType)
        {
            base.OnActorHurt(score, attacker, defender, damage, damageType);

            if (defender.ObjectId != ActorId && ArtificialIntelligenceService.IsSameTeam(_team, defender.Team))
            {
                score.Penalize(damage * 10, RewardType.TeamHurt);
            }
        }
    }
}