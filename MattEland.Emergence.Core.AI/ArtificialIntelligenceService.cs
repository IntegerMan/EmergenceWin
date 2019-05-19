using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeneticSharp.Domain.Fitnesses;
using MattEland.Emergence.AI.Brains;
using MattEland.Emergence.AI.Genetics;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI
{
    public class ArtificialIntelligenceService : IArtificialIntelligenceService
    {
        private readonly IDictionary<string, IBrain> _brains;
        private readonly IBrainProvider _fallbackProvider;

        public ArtificialIntelligenceService(IBrainProvider fallbackProvider)
        {
            _brains = new Dictionary<string, IBrain>();

            _fallbackProvider = fallbackProvider;
        }

        private static IBrain CreateRandomBrain(string id)
        {
            var chromosome = CellInterpretationGeneGenerator.GenerateChromosome();

            var brain = new GeneticBrain(chromosome, id);

            return brain;
        }

        public IEnumerable<string> GetBrainIds()
        {
            return _brains.Keys;
        }

        public IBrain GetBrain(string id, string azureConnStr)
        {
            if (_brains.ContainsKey(id))
            {
                return _brains[id];
            }

            var chromosome = CellInterpretationGeneGenerator.GenerateChromosome();
            
            var brain = BuildBrainForActor(id, chromosome);

            _brains[id] = brain;

            return brain;
        }

        public static IBrain BuildBrainForActor(string id, CellInterpretationChromosome chromosome)
        {
            IBrain brain = id == "ACTOR_LOGIC_BOMB"
                ? new LogicBombGeneticBrain(chromosome, id)
                : new GeneticBrain(chromosome, id);
            return brain;
        }

        public IBrain Train(IBrain brain, IFitness fitness, string brainId, int popSize, int numGenerations)
        {
            return TrainStatic(brain, fitness, brainId, popSize, numGenerations);
        }

        public static IBrain TrainStatic(IBrain brain, IFitness fitness, string brainId, int popSize, int numGenerations)
        {
            var trainer = new BrainTrainer();

            // If the brain isn't genetic, switch to a randomized genetic one and let it fly
            if (!(brain is GeneticBrain geneBrain))
            {
                geneBrain = (GeneticBrain) CreateRandomBrain(brain.Id);
                brain = geneBrain;
            }

            var chromosome = (CellInterpretationChromosome) trainer.Train(popSize, numGenerations, geneBrain.Chromosome, fitness, brainId);

            geneBrain.Chromosome = chromosome;
            
            return brain;
        }

        public static bool IsSameTeam(Alignment attackerTeam, Alignment defenderTeam)
        {
            if (attackerTeam == defenderTeam) return true;

            switch (attackerTeam)
            {
                case Alignment.Neutral:
                    break;
                case Alignment.SystemCore:
                case Alignment.SystemAntiVirus:
                case Alignment.SystemSecurity:
                    return defenderTeam == Alignment.SystemSecurity || defenderTeam == Alignment.SystemCore || defenderTeam == Alignment.SystemAntiVirus;

                case Alignment.Virus:
                    return defenderTeam == Alignment.Bug;

                case Alignment.Bug:
                    return defenderTeam == Alignment.Virus;

                case Alignment.Player:
                    break;
            }

            return false;
        }

        public static bool IsOpposingTeam(Alignment attackerTeam, Alignment defenderTeam)
        {
            if (attackerTeam == defenderTeam) return true;

            switch (attackerTeam)
            {
                case Alignment.Neutral:
                case Alignment.SystemSecurity:
                case Alignment.SystemCore:
                    return defenderTeam == Alignment.Player || defenderTeam == Alignment.Bug;

                case Alignment.SystemAntiVirus:
                    return defenderTeam == Alignment.Player || defenderTeam == Alignment.Bug || defenderTeam == Alignment.Virus;

                case Alignment.Virus:
                case Alignment.Bug:
                    return defenderTeam != Alignment.Bug && defenderTeam != Alignment.Virus;

                case Alignment.Player:
                    return true;
            }

            return false;
        }


        public void CarryOutMove(ICommandContext context, IActor actor, IBrain brain)
        {

            if (brain != null)
            {
                IGameCell choice = null;

                if (!brain.HandleSpecialCommand(context, actor))
                {
                    var visible = actor.VisibleCells.Select(c => context.Level.GetCell(c)).Where(c => c != null).ToList();

                    var choices = new List<IGameCell>
                    {
                        context.Level.GetCell(actor.Position)
                    };

                    if (!actor.IsImmobile)
                    {
                        choices.Add(context.Level.GetCell(actor.Position.Add(1, 0)));
                        choices.Add(context.Level.GetCell(actor.Position.Add(-1, 0)));
                        choices.Add(context.Level.GetCell(actor.Position.Add(0, 1)));
                        choices.Add(context.Level.GetCell(actor.Position.Add(0, -1)));
                    }

                    choices = choices.Where(c => c != null && (!c.HasNonActorObstacle || c.Objects.Any(o => o.IsInteractive)) && (c.Actor == null || c.Pos == actor.Position || !IsSameTeam(actor.Team, c.Actor.Team)))
                                     .ToList();
                    choice = brain.GetActorChoice(actor, choices, visible, context);

                    // Training framework needs to know about this
                    OnActorChoiceMade?.Invoke(context, actor, choice);

                    if (choice != null && choice.Pos != actor.Position)
                    {
                        context.GameService.HandleCellMoveCommand(context, actor, choice.Pos);
                    }
                    else
                    {
                        actor.OnWaited(context);
                    }

                }

                brain.UpdateActorState(context, actor, choice);

            }
        }

        public void ClearBrainCache()
        {
            _brains.Clear();
        }

        public Action<ICommandContext, IActor, IGameCell> OnActorChoiceMade { get; set; }

        public IBrain GetBrainForActor(IActor actor)
        {
            // Find a team / actor-specific brain if possible
            var key = actor.ObjectId;

            if (actor.IsCorrupted)
            {
                key = "ACTOR_BUG";
            }

            if (_brains.ContainsKey(key))
            {
                return _brains[key];
            }

            // No brain for immobile actors, unless one is explicitly provided
            if (actor.IsImmobile)
            {
                return null;
            }

            // Get and store the brain
            var brain = _fallbackProvider.GetBrainForActor(actor);
            _brains[key] = brain; // Storing null values is cool here - we don't want to keep re-querying for it

            return brain;
        }

    }
}