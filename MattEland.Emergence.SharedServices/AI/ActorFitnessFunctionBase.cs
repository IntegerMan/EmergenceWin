using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;
using MattEland.Emergence.AI;
using MattEland.Emergence.AI.Brains;
using MattEland.Emergence.AI.Genetics;
using MattEland.Emergence.AI.Sensory;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using Microsoft.Extensions.Logging;

namespace MattEland.Emergence.Services.AI
{
    public abstract class ActorFitnessFunctionBase : IFitness
    {
        private readonly ICommandContextGenerator _contextGenerator;
        private readonly IBrainProvider _fallbackProvider;
        private readonly GameSimulator _simulator;
        private readonly AlwaysOnSimulationManager _simManager;

        protected ActorFitnessFunctionBase(ICommandContextGenerator contextGenerator,
            IArtificialIntelligenceService aiService,
            string actorId,
            ILoggerFactory logFactory)
        {
            _contextGenerator = contextGenerator;
            Logger = logFactory.CreateLogger(GetType());
            ActorId = actorId;
            _fallbackProvider = new LegacyBrainProvider();

            _simManager = new AlwaysOnSimulationManager();
            _simulator = new GameSimulator(aiService);

            // Set up our turn count per simulation
            _turnsPerRun[0] = 15;
            _turnsPerRun[1] = 35;
            _turnsPerRun[2] = 10;
            _turnsPerRun[3] = 10;
            _turnsPerRun[4] = 10;
            _turnsPerRun[5] = 10;
            _turnsPerRun[6] = 10;
            _turnsPerRun[7] = 10;
        }

        public string ActorId { get; }

        public ILogger Logger { get; }

        protected virtual int NumRuns => 8;

        public double Evaluate(IChromosome chromosome)
        {
            // Build out a brain based on the chromosome being evaluated
            var cellChromosome = (CellInterpretationChromosome) chromosome;
            cellChromosome.TelemetrySeries = new List<TelemetrySeries>
            {
                new TelemetrySeries {Title = "X"},
                new TelemetrySeries {Title = "Y"},
                new TelemetrySeries {Title = "Stability"},
                new TelemetrySeries {Title = "Damage Dealt"},
                new TelemetrySeries {Title = "Damage Received"},
                new TelemetrySeries {Title = "Corruption"},
                new TelemetrySeries {Title = "Sys Cores"},
                //new TelemetrySeries {Title = "Reward"},
            };
            var brain = ArtificialIntelligenceService.BuildBrainForActor(ActorId, cellChromosome);
            var provider = new FitnessBrainProvider(brain, ActorId, _fallbackProvider);

            decimal aggregateFitness = 0;

            var sources = new Dictionary<RewardType, decimal>();

            for (int runNumber = 0; runNumber < NumRuns; runNumber++)
            {
                // Grab a simulation context based around fresh level data
                var context = GetEvaluationContext(runNumber);

                var score = new FitnessTracker(context);

                // Configure the context
                context.OnActorHurt += (sender, args) =>
                {
                    OnActorHurt(score, args.Attacker, args.Defender, args.Damage, args.DamageType);
                };
                var actors = context.Level.Actors.Where(a => a.ObjectId == ActorId).ToList();

                // Simulate a number of game turns
                for (int turn = 0; turn < _turnsPerRun[runNumber]; turn++)
                {
                    // Simulate a game move with instructions on how each actor should behave (including the player)
                    _simulator.SimulateGameTurn(context, provider, _simManager);

                    // Keep a running total of fitness
                    ScoreActors(score, context, actors, cellChromosome.TelemetrySeries);
                }

                // Average out our scores and come up with a composite fitness score for the run
                score.DivideBy(_turnsPerRun[runNumber]);
                foreach (var kvp in score.Sources)
                {
                    if (sources.ContainsKey(kvp.Key))
                    {
                        sources[kvp.Key] += kvp.Value;
                    }
                    else
                    {
                        sources[kvp.Key] = kvp.Value;
                    }
                }
                
                aggregateFitness += score.Fitness;
            }



            // Calculate and return an aggregate value
            chromosome.Fitness = (double)(aggregateFitness / NumRuns);
            cellChromosome.UpdateFitnessBreakdown(sources);

            return chromosome.Fitness.Value;
        }

        private readonly IDictionary<int, int> _turnsPerRun = new Dictionary<int, int>();

        private ICommandContext GetEvaluationContext(int runNumber)
        {
            ICommandContext context;

            if (runNumber == 0 || runNumber == 7)
            {
                context = _contextGenerator.Generate(LevelType.TrainSingleRoom);
            }
            else if (runNumber == 1)
            {
                context = _contextGenerator.Generate(LevelType.TrainTwinRooms);
            }
            else
            {
                context = _contextGenerator.Generate(LevelType.TrainFriendlyFire);
            }

            context.Randomizer = new BasicRandomization();

            context.SetLevel(GetLevelForEvaluation(context, runNumber));
            return context;
        }

        protected virtual ILevel GetLevelForEvaluation(ICommandContext context, int runNumber)
        {

            if (runNumber >= 2 && runNumber <= 6)
            {
                context.Level.AddObject(
                    CreationService.CreateObject(ActorId, GameObjectType.Actor, new Pos2D(3, 1)));

                string actorId = ActorId;
                switch (runNumber)
                {
                    case 2:
                        actorId = "ACTOR_BUG";
                        break;
                    case 3:
                        actorId = "ACTOR_BIT";
                        break;
                    case 4:
                        actorId = "ACTOR_VIRUS";
                        break;
                    case 5:
                        actorId = "ACTOR_ANTI_VIRUS";
                        break;
                    case 6:
                        actorId = "ACTOR_SEC_AGENT";
                        break;
                }
                context.Level.AddObject(
                    CreationService.CreateObject(actorId, GameObjectType.Actor, new Pos2D(4, 1)));
            }
            else if (runNumber == 7)
            {
                context.Level.AddObject(CreationService.CreateObject(ActorId, GameObjectType.Actor, new Pos2D(5, 5)));
            }
            else
            {
                if (runNumber == 0)
                {
                    context.Level.AddObject(
                        CreationService.CreateObject(ActorId, GameObjectType.Actor, new Pos2D(5, 7)));
                }
                else if (runNumber == 1)
                {
                    context.Level.AddObject(
                        CreationService.CreateObject(ActorId, GameObjectType.Actor, new Pos2D(17, 5)));
                }

                string otherActorId = "ACTOR_BUG";
                if (ActorId == "ACTOR_BUG" || ActorId == "ACTOR_VIRUS" || ActorId == "ACTOR_WORM" ||
                    ActorId == "ACTOR_GLITCH")
                {
                    otherActorId = "ACTOR_SEC_AGENT";
                }

                context.Level.AddObject(
                    CreationService.CreateObject(otherActorId, GameObjectType.Actor, new Pos2D(1, 1)));
            }

            // Immobilize everyone else
            foreach (var actor in context.Level.Actors.Where(a => a.ObjectId != ActorId))
            {
                actor.IsImmobile = true;
            }

            // Corrupt everything if we're the AV actor
            if (ActorId == "ACTOR_ANTI_VIRUS")
            {
                foreach (var cell in context.Level.Cells)
                {
                    cell.Corruption = 1;
                }
            }

            return context.Level;
        }

        protected virtual void OnActorHurt(FitnessTracker score, 
                                           IGameObject attacker, 
                                           IGameObject defender, 
                                           decimal damage, 
                                           DamageType damageType)
        {
            if (attacker.ObjectId == ActorId)
            {
                // Always penalize team kills
                if (ArtificialIntelligenceService.IsSameTeam(attacker.Team, defender.Team))
                {
                    score.PenalizeTeamAttack(damage);
                }
                else
                {
                    score.Reward(damage * 50, RewardType.Attack);
                }

                if (defender.IsPlayer)
                {
                    score.Reward(50 * damage, RewardType.AttackedPlayer);
                }
            }
        }

        private void ScoreActors(FitnessTracker fitness,
                                 ICommandContext context,
                                 ICollection<IActor> actors,
                                 IList<TelemetrySeries> telemetrySeries)
        {
            var actor = actors.First();
            ScoreActor(fitness, context, actor);

            telemetrySeries.First(t => t.Title == "X").Values.Add(actor.Position.X);
            telemetrySeries.First(t => t.Title == "Y").Values.Add(actor.Position.Y);
            telemetrySeries.First(t => t.Title == "Stability").Values.Add(actor.Stability);
            telemetrySeries.First(t => t.Title == "Damage Dealt").Values.Add(actor.DamageDealt);
            telemetrySeries.First(t => t.Title == "Damage Received").Values.Add(actor.DamageReceived);
            telemetrySeries.First(t => t.Title == "Sys Cores").Values.Add(context.Level.Cores.Count(c => c.Team == Alignment.SystemCore || c.Team == Alignment.SystemSecurity || c.Team == Alignment.SystemAntiVirus) * 5);
            telemetrySeries.First(t => t.Title == "Corruption").Values.Add((context.Level.Cells.Sum(c => c.Corruption) / 5.0m) + context.Level.Actors.Where(a => a.IsCorruptable && a.IsCorrupted).Sum(a => a.Corruption));
            //telemetrySeries.First(t => t.Title == "Reward").Values.Add(fitness.CurrentTurnReward / 50);
            fitness.ClearCurrentTurnCounters();
        }

        protected abstract FitnessTracker ScoreActor(FitnessTracker score,
                                                     ICommandContext context,
                                                     IActor actor);

    }
}