using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class FitnessTracker
    {
        private readonly ICommandContext _context;
        private readonly List<IActor> _cores;
        private readonly List<IGameObject> _corruptable;
        private readonly IDictionary<RewardType, decimal> _sources = new Dictionary<RewardType, decimal>();

        public decimal Fitness { get; set; }

        public FitnessTracker(ICommandContext context)
        {
            _context = context;

            _cores = _context.Level.Cores.ToList();
            _corruptable = _context.Level.Objects.Where(o => o.IsCapturable).ToList();
        }

        public void DivideBy(int divisor)
        {
            Fitness /= divisor;
        }

        public FitnessTracker AdjustScoreByCoresControlled(IActor actor)
        {
            int numCores = _cores.Count;
            if (numCores == 0) return this;

            int ownedCores = _cores.Count(c => c.Team == actor.Team);
            Reward((ownedCores / numCores) * 10, RewardType.CoresControlled);

            return this;
        }

        public FitnessTracker AdjustScoreByMovedRemainedStationary(IActor actor, decimal reward = 1)
        {
            if (actor.IsDead)
            {
                return this;
            }

            // Incentivize moving around
            Reward(actor.KnownCells.Count * reward, RewardType.Moved);

            int i = 0;
            foreach (var pos in actor.RecentPositions)
            {
                if (pos == actor.Pos)
                {
                    Penalize(1, RewardType.RemainedStationary);
                }
                i++;
            }

            return this;
        }

        public FitnessTracker AdjustScoreByDamageGivenReceived(IActor actor)
        {
            Penalize(actor.DamageReceived * 5, RewardType.DamageReceived);
            /*
            Reward(actor.DamageDealt * 15);
            */

            return this;
        }

        public FitnessTracker AdjustScoreByKamakaziFactor(IActor actor)
        {
            Reward(actor.DamageDealt * 25, RewardType.SelfDestruct);
            if (actor.IsDead)
            {
                Reward(5, RewardType.SelfDestruct);
            }

            return this;
        }

        public FitnessTracker AdjustScoreByStability(IActor actor, int deadPenalty = 25, decimal stabilityBonus = 5)
        {
            if (actor.IsDead)
            {
                Penalize(deadPenalty, RewardType.Dead);
            }
            else
            {
                Reward(stabilityBonus * (actor.Stability / (decimal)actor.MaxStability), RewardType.Stability);
            }

            if (actor.IsCorruptable && actor.Corruption > 0)
            {
                Penalize(actor.Corruption * 50, RewardType.SelfCorrupt);
            }

            return this;
        }

        public FitnessTracker AdjustScoreByCorruptObjects(decimal multiplier)
        {
            foreach (var obj in _corruptable.Where(o => o.IsCorrupted))
            {
                Reward(obj.Corruption * multiplier, RewardType.CorruptObjects);
            }

            return this;
        }

        public FitnessTracker AdjustScoreByCorruptArea(decimal multiplier)
        {
            foreach (var levelCell in _context.Level.Cells)
            {
                Reward(levelCell.Corruption * multiplier, RewardType.CorruptArea);
            }

            return this;
        }

        public FitnessTracker PenalizeTeamAttack(decimal damage)
        {
            return Penalize(100 * damage, RewardType.TeamAttack);
        }

        public FitnessTracker Penalize(decimal amount, RewardType rewardType)
        {
            return Reward(-amount, rewardType);
        }

        public FitnessTracker Reward(decimal amount, RewardType rewardType)
        {
            if (amount != 0)
            {

                Fitness += amount;
                CurrentTurnReward += amount;

                if (!_sources.ContainsKey(rewardType))
                {
                    _sources[rewardType] = amount;
                }
                else
                {
                    _sources[rewardType] += amount;
                }
            }

            return this;
        }

        public FitnessTracker AdjustScoreByActorVisibleTileCount(IActor actor, decimal multiplier)
        {
            if (actor.IsDead)
            {
                return this;
            }

            if (actor.VisibleCells == null)
            {
                _context.CalculateLineOfSight(actor);
            }

            if (actor.VisibleCells != null)
            {
                Reward(actor.VisibleCells.Count * multiplier, RewardType.CellVisible);
            }

            return this;
        }

        public FitnessTracker AdjustScoreByVisibleNonSystemEntities(IActor actor)
        {
            if (actor.IsDead)
            {
                return this;
            }

            foreach (var cell in actor.VisibleCells)
            {
                var observed = _context.Level.Actors.FirstOrDefault(a => a.Pos == cell);
                if (observed != null &&
                    observed.Team != Alignment.SystemAntiVirus && 
                    observed.Team != Alignment.SystemCore &&
                    observed.Team != Alignment.SystemSecurity)
                {
                    Reward(50, RewardType.SpottedActor);
                }
            }
            return this;
        }

        public FitnessTracker AdjustScoreByProximityToPlayer(IActor actor, decimal multiplier)
        {
            if (actor.IsDead)
            {
                return this;
            }

            if (_context.Player != null)
            {
                decimal distance = (decimal) _context.Player.Pos.CalculateDistanceFrom(actor.Pos);

                if (distance <= 10)
                {
                    Reward(((25m - distance) * multiplier), RewardType.PlayerProximity);
                }
            }

            return this;
        }

        public decimal CurrentTurnReward { get; private set; }
        public IDictionary<RewardType, decimal> Sources => _sources;

        public void ClearCurrentTurnCounters()
        {
            CurrentTurnReward = 0;
        }
    }
}