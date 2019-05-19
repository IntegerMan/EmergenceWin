using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using MattEland.Emergence.AI.Sensory;

namespace MattEland.Emergence.AI.Genetics
{
    public class CellInterpretationChromosome : IChromosome
    {
        public IDictionary<RewardType, decimal> FitnessSources { get; private set; }

        public IList<Gene> Genes { get; }

        public CellInterpretationChromosome()
        {
            Genes = new List<Gene>();
        }

        public void ReplaceGene(int index, Gene gene)
        {
            Genes[index] = gene;
        }

        public void ReplaceGenes(int startIndex, Gene[] genes)
        {
            while (Genes.Count > startIndex)
            {
                Genes.RemoveAt(startIndex);
            }

            foreach (var gene in genes)
            {
                Genes.Add(gene);
            }
        }

        public void Resize(int newLength)
        {
            while (Genes.Count > newLength)
            {
                Genes.RemoveAt(Genes.Count - 1);
            }

            while (Genes.Count < newLength)
            {
                Genes.Add(GenerateGene(Genes.Count));
            }
        }

        public Gene GetGene(int index)
        {
            return Genes[index];
        }

        public Gene[] GetGenes()
        {
            return Genes.ToArray();
        }

        public Gene GenerateGene(int index)
        {
            return CellInterpretationGeneGenerator.GenerateGene();
        }

        public IChromosome CreateNew()
        {
            return CellInterpretationGeneGenerator.GenerateChromosome();
        }

        public IChromosome Clone()
        {
            var clone = new CellInterpretationChromosome();

            clone.Genes.Clear();
            foreach (var gene in Genes)
            {
                clone.Genes.Add(gene);
            }

            clone.Fitness = Fitness;

            return clone;
        }

        public double? Fitness { get; set; }

        public int Length => Genes.Count;
        public IList<TelemetrySeries> TelemetrySeries { get; set; }

        public int CompareTo(IChromosome other)
        {
            return Length - other.Length; // TODO: might need to use something more specific
        }

        public static CellInterpretationChromosome TranslateFromJsonChromosome(CellInterpretationChromosome jsonChromosome)
        {
            // Due to Newtonsoft JSON deserialization quirks, some nested objects get translated as JObjects,
            // which causes downstream issues. Translate to actual objects.
            var actualChromosome = new CellInterpretationChromosome();
            actualChromosome.Genes.Clear();
            foreach (var jsonGene in jsonChromosome.Genes)
            {
                var weight =  (double)jsonGene.Value;

                actualChromosome.Genes.Add(new Gene((decimal)weight));
            }

            return actualChromosome;
        }

        public void UpdateFitnessBreakdown(Dictionary<RewardType, decimal> sources)
        {
            FitnessSources = sources;
        }
    }
}