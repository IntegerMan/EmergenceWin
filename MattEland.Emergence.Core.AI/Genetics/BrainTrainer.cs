using System;
using System.Threading;
using GeneticSharp.Domain;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Populations;
using GeneticSharp.Domain.Selections;
using GeneticSharp.Domain.Terminations;
using GeneticSharp.Infrastructure.Framework.Threading;

namespace MattEland.Emergence.AI.Genetics
{
    public class BrainTrainer
    {

        public IChromosome Train(int populationSize, int numGenerations, IChromosome adamChromosome, IFitness fitness, string brainId, Action<GeneticAlgorithm> progressCallback = null)
        {
            var ga = SetupAlgorithm(populationSize, numGenerations, adamChromosome, fitness, brainId, progressCallback);

            ga.Start();
            while (ga.IsRunning)
            {
                Thread.Sleep(50);
            }

            return ga.BestChromosome;
        }

        public GeneticAlgorithm SetupAlgorithm(int populationSize, int numGenerations, IChromosome adamChromosome, IFitness fitness, string brainId, Action<GeneticAlgorithm> progressCallback = null)
        {
            if (adamChromosome.Length < 2)
            {
                throw new ArgumentException("Chromosomes must have at least two genes", nameof(adamChromosome));
            }

            var population = new Population(populationSize, populationSize, adamChromosome)
            {
                GenerationStrategy = new PerformanceGenerationStrategy()
            };

            var selection = new EliteSelection();
            var mutation = new WeightMutation();

            var ga = new GeneticAlgorithm(population, fitness, selection, new EliteOrientedCrossover(mutation), mutation)
            {
                TaskExecutor = new ParallelTaskExecutor(),
                Termination = new GenerationNumberTermination(numGenerations)
            };

            if (progressCallback != null)
            {
                ga.GenerationRan += (o, args) => progressCallback.Invoke(ga);
            }

            return ga;
        }
    }
}