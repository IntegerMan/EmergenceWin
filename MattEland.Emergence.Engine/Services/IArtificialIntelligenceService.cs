using System.Collections.Generic;
using GeneticSharp.Domain.Fitnesses;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
{
    public interface IArtificialIntelligenceService : IBrainProvider
    {
        IEnumerable<string> GetBrainIds();
        IBrain GetBrain(string id, string azureConnStr);
        IBrain Train(IBrain brain, IFitness fitness, string brainId, int popSize, int numGenerations);
        void CarryOutMove(CommandContext context, Actor actor, IBrain brain);

        /// <summary>
        /// Clears all cached brains so they will need to be reloaded at the provider level
        /// </summary>
        void ClearBrainCache();
    }
}