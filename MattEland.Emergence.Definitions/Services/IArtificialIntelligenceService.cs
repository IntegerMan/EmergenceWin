using System.Collections.Generic;
using GeneticSharp.Domain.Fitnesses;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface IArtificialIntelligenceService : IBrainProvider
    {
        IEnumerable<string> GetBrainIds();
        IBrain GetBrain(string id, string azureConnStr);
        IBrain Train(IBrain brain, IFitness fitness, string brainId, int popSize, int numGenerations);
        void CarryOutMove(ICommandContext context, IActor actor, IBrain brain);

        /// <summary>
        /// Clears all cached brains so they will need to be reloaded at the provider level
        /// </summary>
        void ClearBrainCache();
    }
}