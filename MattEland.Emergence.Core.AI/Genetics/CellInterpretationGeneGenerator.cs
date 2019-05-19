using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using MattEland.Emergence.AI.Brains;
using MattEland.Emergence.AI.Sensory;

namespace MattEland.Emergence.AI.Genetics
{
    public static class CellInterpretationGeneGenerator
    {
        private static readonly Random _rng = new Random();

        public static Gene GenerateGene()
        {
            return new Gene(GetRandomWeight());
        }

        public static decimal GetRandomWeight()
        {
            return ((_rng.Next(0, 21) - 10) / 10m);
        }

        public static CellInterpretationChromosome GenerateChromosome()
        {
            var chromosome = new CellInterpretationChromosome();

            var genes = new List<Gene>();

            // Input layer
            var numInputs = Enum.GetValues(typeof(CellAspectType)).Length;
            var numConnections = (numInputs * GeneticBrain.HiddenLayerSize);

            // Hidden layer(s)
            for (var layer = 0; layer < GeneticBrain.HiddenLayerCount - 1; layer++)
            {
                numConnections += GeneticBrain.HiddenLayerSize * GeneticBrain.HiddenLayerSize;
            }

            // Outputs
            numConnections += GeneticBrain.HiddenLayerSize;
            
            for (var i = 0; i < numConnections; i++)
            {
                genes.Add(new Gene(GetRandomWeight()));
            }

            chromosome.ReplaceGenes(0, genes.ToArray());

            return chromosome;
        }
    }
}