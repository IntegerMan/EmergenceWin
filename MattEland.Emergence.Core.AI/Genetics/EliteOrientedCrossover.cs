using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Crossovers;
using GeneticSharp.Domain.Mutations;

namespace MattEland.Emergence.AI.Genetics
{
    public class EliteOrientedCrossover : ICrossover
    {
        private readonly IMutation _mutator;

        public EliteOrientedCrossover(IMutation mutator)
        {
            _mutator = mutator;
        }

        public bool IsOrdered => false;
        public int ParentsNumber => 1;
        public int ChildrenNumber => 5;
        public int MinChromosomeLength => 0;

        public IList<IChromosome> Cross(IList<IChromosome> parents)
        {
            var list = new List<IChromosome>();

            var chromosome = parents.First();

            // Always include the base chromosome
            list.Add(chromosome.Clone());

            // Add some additional chromosomes that have varying degrees of mutation
            list.Add(CloneAndMutate(chromosome, 0.01f));
            list.Add(CloneAndMutate(chromosome, 0.03f));
            list.Add(CloneAndMutate(chromosome, 0.05f));

            // Add a completely random chromosome
            list.Add(chromosome.CreateNew());

            return list;
        }

        private IChromosome CloneAndMutate(IChromosome chromosome, float geneMutateProbability)
        {
            var clone = chromosome.Clone();
            _mutator.Mutate(clone, geneMutateProbability);

            return clone;
        }
    }
}
