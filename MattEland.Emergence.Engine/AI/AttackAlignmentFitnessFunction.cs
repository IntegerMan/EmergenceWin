using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class AttackAlignmentFitnessFunction : ActorFitnessFunctionBase
    {

        public AttackAlignmentFitnessFunction(ICommandContextGenerator contextGenerator,
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
                        .AdjustScoreByMovedRemainedStationary(actor)
                        .AdjustScoreByProximityToPlayer(actor, 2)
                        .AdjustScoreByDamageGivenReceived(actor);
        }

        protected override void OnActorHurt(FitnessTracker score,
                                            IGameObject attacker,
                                            IGameObject defender,
                                            decimal damage,
                                            DamageType damageType)
        {
            base.OnActorHurt(score, attacker, defender, damage, damageType);

            if (attacker.ObjectId == ActorId)
            {
                if (ArtificialIntelligenceService.IsOpposingTeam(attacker.Team, defender.Team))
                {
                    score.Reward(damage * 50, RewardType.Attack);
                }
                else
                {
                    score.Penalize(damage * 5, RewardType.TeamAttack);
                }
            }
        }
    }
}