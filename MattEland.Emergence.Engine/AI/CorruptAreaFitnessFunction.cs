using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class CorruptAreaFitnessFunction : ActorFitnessFunctionBase
    {

        public CorruptAreaFitnessFunction(ICommandContextGenerator contextGenerator,
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
                        .AdjustScoreByCorruptArea(25);
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
                if ((defender.IsCorruptable && !defender.IsCorrupted) || (defender.Team != Alignment.Bug && defender.Team != Alignment.Virus))
                {
                    score.Reward(damage * 25, RewardType.Attack);
                }
                else
                {
                    score.Penalize(damage * 50, RewardType.TeamAttack);
                }
            }
        }
    }
}