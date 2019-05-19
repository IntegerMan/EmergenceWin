using System;
using System.Collections.Generic;
using System.Linq;
using MattEland.AI.Neural;
using MattEland.Emergence.AI.Genetics;
using MattEland.Emergence.AI.Sensory;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI.Brains
{
    public class GeneticBrain : ActorBrainBase
    {
        private readonly NeuralNet _net;
        private CellInterpretationChromosome _chromosome;

        public const int HiddenLayerCount = 2;
        public const int HiddenLayerSize = 10;

        public GeneticBrain(CellInterpretationChromosome chromosome, string id)
        {
            Id = id;

            // Set up the neural net. This has to happen before setting the Chromosome
            _net = new NeuralNet(Enum.GetValues(typeof(CellAspectType)).Length, 1);

            // Add hidden layers
            for (int i = 0; i < HiddenLayerCount; i++)
            {
                _net.AddHiddenLayer(HiddenLayerSize);
            }

            _net.Connect();

            Chromosome = chromosome;
        }

        public override string Id { get; }

        protected override decimal CalculateCellScore(IGameCell choice, IActor actor, IEnumerable<IGameCell> otherCells, ICommandContext context)
        {
            var interpretation = CellInterpreter.Interpret(choice, actor, otherCells.ToList(), context);

            var inputs = interpretation.Aspects.Select(a => a.Value).ToList();

            // Crunch some numbers and get back a result. We expect 1 output neuron
            var outputValues = _net.Evaluate(inputs);
            var fitness = outputValues.First();

            return fitness;
        }

        private void ApplyWeightsToNeuralNet()
        {
            var genes = Chromosome.Genes.Select(g => (decimal)g.Value);

            _net.SetWeights(genes.ToList());
        }

        public CellInterpretationChromosome Chromosome
        {
            get => _chromosome;
            set
            {
                _chromosome = value;

                // Any time the chromosome changes, we need to tweak the weights
                ApplyWeightsToNeuralNet();
            }
        }

        public NeuralNet NeuralNet => _net;
    }
}