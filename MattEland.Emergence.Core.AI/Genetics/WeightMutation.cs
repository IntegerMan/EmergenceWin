using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Mutations;
using GeneticSharp.Domain.Randomizations;

namespace MattEland.Emergence.AI.Genetics
{
    public class WeightMutation : IMutation
    {
        public bool IsOrdered => false;

        public void Mutate(IChromosome chromosome, float probability)
        {
            var genes = chromosome.GetGenes().ToList();
            var randomGenes = genes.OrderBy(g => RandomizationProvider.Current.GetDouble());

            // Mutate a random number of genes on the chromosome
            var numMutations = RandomizationProvider.Current.GetInt(1, genes.Count - 1);

            var toMutate = randomGenes.Take(numMutations);
            foreach (var gene in toMutate)
            {
                chromosome.ReplaceGene(genes.IndexOf(gene), CellInterpretationGeneGenerator.GenerateGene());
            }

        }
    }
}