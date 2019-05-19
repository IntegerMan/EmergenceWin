using GeneticSharp.Domain.Fitnesses;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using Microsoft.Extensions.Logging;

namespace MattEland.Emergence.Services.AI
{
    public class FitnessFunctionFactory
    {
        private readonly ICommandContextGenerator _contextGenerator;
        private readonly IArtificialIntelligenceService _aiService;
        private readonly ILoggerFactory _logFactory;

        public FitnessFunctionFactory(ICommandContextGenerator contextGenerator, 
                                      IArtificialIntelligenceService aiService, 
                                      ILoggerFactory logFactory)
        {
            _contextGenerator = contextGenerator;
            _aiService = aiService;
            _logFactory = logFactory;
        }

        public FitnessFunctionType GetFitnessFunctionTypeForActor(string actorId)
        {

            switch (actorId)
            {
                case "ACTOR_KERNEL_WORKER":
                    return FitnessFunctionType.ControlCores;

                case "ACTOR_DEFENDER":
                case "ACTOR_SEC_AGENT":
                    return FitnessFunctionType.DefendSystem;

                case "ACTOR_ANTI_VIRUS":
                    return FitnessFunctionType.CleanseCorruption;

                case "ACTOR_LOGIC_BOMB":
                    return FitnessFunctionType.LogicBomb;

                case "ACTOR_HELP":
                    return FitnessFunctionType.AnnoyPlayer;

                case "ACTOR_INSPECTOR":
                    return FitnessFunctionType.MaximizeVisibility;

                case "ACTOR_GLITCH":
                    return FitnessFunctionType.AttackSystem;

                case "ACTOR_DAEMON":
                case "ACTOR_GARBAGE_COLLECTOR":
                    return FitnessFunctionType.AttackNonSystem;

                case "ACTOR_WORM":
                    return FitnessFunctionType.CorruptArea;

                case "ACTOR_VIRUS":
                    return FitnessFunctionType.CorruptActors;

                case "ACTOR_BUG":
                case "ACTOR_FEATURE":
                    return FitnessFunctionType.MaximizeChaos;

                case "ACTOR_BIT":
                default:
                    return FitnessFunctionType.Survival;
            }
        }

        public IFitness BuildFitnessFunction(FitnessFunctionType fitnessType, string actorId)
        {
            switch (fitnessType)
            {
                case FitnessFunctionType.CorruptArea:
                    return new CorruptAreaFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.LogicBomb:
                    return new LogicBombFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.CorruptActors:
                    return new CorruptActorsFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.CleanseCorruption:
                    return new CleanseCorruptionFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.ControlCores:
                    return new ControlCoresFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.AttackNonSystem:
                    return new AttackAlignmentFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.AttackSystem:
                    return new AttackAlignmentFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.MaximizeChaos:
                    return new MaximizeChaosFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.DefendSystem:
                    return new DefendAlignmentFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory, Alignment.SystemSecurity);

                case FitnessFunctionType.MaximizeVisibility:
                    return new MaximizeVisibilityFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.AnnoyPlayer:
                    return new AnnoyPlayerFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);

                case FitnessFunctionType.Survival:
                default:
                    return new ActorSurvivalFitnessFunction(_contextGenerator, _aiService, actorId, _logFactory);
            }
        }
    }
}