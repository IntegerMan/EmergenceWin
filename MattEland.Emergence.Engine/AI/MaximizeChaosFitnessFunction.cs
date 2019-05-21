using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class MaximizeChaosFitnessFunction : ActorFitnessFunctionBase
    {
        public MaximizeChaosFitnessFunction(ICommandContextGenerator contextGenerator,
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
                        .AdjustScoreByCorruptArea(1)
                        .AdjustScoreByCorruptObjects(1)
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
                    score.Reward(damage * 25, RewardType.Attack);
                }
                else
                {
                    score.Penalize(damage, RewardType.TeamAttack);
                }
            }
        }
    }
}