using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI.Brains
{
    internal class PreyBrain : ActorBrainBase
    {
        private readonly ISet<Alignment> _knownPredators;
        public PreyBrain(params Alignment[] knownPredators)
        {
            _knownPredators = new HashSet<Alignment>(knownPredators);
        }

        public override string Id => "STATIC_BRAIN_PREY";
        protected override decimal CalculateCellScore(IGameCell choice, IActor actor, IEnumerable<IGameCell> otherCells, ICommandContext context)
        {
            // Avoid walls
            if (choice.HasNonActorObstacle)
            {
                return -50;
            }

            // Avoid other actors
            if (choice.Actor != null && choice.Actor != actor)
            {
                return -25;
            }

            // TODO: Prioritize fleeing from spaces adjacent to predators once "smells" are implemented

            // Feel indifferent about this score
            return 0;
        }

    }
}