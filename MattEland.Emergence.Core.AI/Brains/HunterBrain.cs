using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI.Brains
{
    /// <summary>
    /// An actor brain that moves randomly until it finds prey, at which point it will attack.
    /// </summary>
    public class HunterBrain : ActorBrainBase
    {
        public ISet<Alignment> Prey { get; }

        public HunterBrain(params Alignment[] targets)
        {
            Prey = new HashSet<Alignment>(targets);
        }

        public override string Id => "STATIC_BRAIN_HUNTER";

        protected override decimal CalculateCellScore(IGameCell choice, IActor actor, IEnumerable<IGameCell> otherCells, ICommandContext context)
        {
            if (choice.HasNonActorObstacle)
            {
                return -50;
            }

            if (choice.Actor != null && choice.Actor != actor)
            {
                if (Prey.Contains(choice.Actor.Team))
                {
                    return 50;
                }

                return -25; // Do not prioritize attacking non-prey actors
            }

            // Feel indifferent about this score
            return 0;
        }
    }
}