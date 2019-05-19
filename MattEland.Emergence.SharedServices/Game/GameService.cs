using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using MattEland.Emergence.LevelGeneration;
using MattEland.Emergence.Services.AI;

namespace MattEland.Emergence.Services.Game
{
    /// <summary>
    /// A service for dealing with high-level game operations such as new games or player moves.
    /// </summary>
    public sealed class GameService : IGameService
    {
        [NotNull] private readonly LevelGenerationService _levelService;
        [NotNull] private readonly IArtificialIntelligenceService _aiService;
        [NotNull] private readonly ILootProvider _lootProvider;
        [NotNull] private readonly ICombatManager _combatManager;
        [NotNull] private readonly IEntityDefinitionService _entityProvider;
        [NotNull] private readonly GameSimulator _simulator;
        [NotNull] private readonly ISimulationManager _simManager;
        [NotNull] private readonly IRandomization _randomization;

        [NotNull]
        public IRandomization Randomization => _randomization;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="levelService">The level service.</param>
        /// <param name="aiService">The artificial intelligence service.</param>
        /// <param name="combatManager">The combat manager</param>
        public GameService([NotNull] LevelGenerationService levelService, 
                           [NotNull] IArtificialIntelligenceService aiService, 
                           [NotNull] ICombatManager combatManager,
                           [NotNull] ILootProvider lootProvider,
                           [NotNull] IEntityDefinitionService entityService,
                           [NotNull] ISimulationManager simManager,
            [NotNull] IRandomization randomization)
        {
            _levelService = levelService ?? throw new ArgumentNullException(nameof(levelService));
            _aiService = aiService ?? throw new ArgumentNullException(nameof(aiService));
            _combatManager = combatManager ?? throw new ArgumentNullException(nameof(combatManager));
            _lootProvider = lootProvider ?? throw new ArgumentNullException(nameof(lootProvider));
            _entityProvider = entityService ?? throw new ArgumentNullException(nameof(entityService));
            _simManager = simManager ?? throw new ArgumentNullException(nameof(simManager));
            _randomization = randomization;

            _simulator = new GameSimulator(aiService);

            GameCreationConfigurator.ConfigureObjectCreation();
        }

        [NotNull]
        public LevelGenerationService LevelService => _levelService;

        [NotNull]
        public IArtificialIntelligenceService AIService => _aiService;

        [NotNull]
        public ILootProvider LootProvider => _lootProvider;

        [NotNull]
        public ICombatManager CombatManager => _combatManager;

        [NotNull]
        public IEntityDefinitionService EntityProvider => _entityProvider;

        [NotNull]
        public GameSimulator Simulator => _simulator;

        [NotNull]
        public ISimulationManager SimManager => _simManager;

        /// <inheritdoc />
        public GameResponse StartNewGame(NewGameParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.CharacterId))
            {
                parameters.CharacterId = "ACTOR_PLAYER_ANTIVIRUS";
            }

#if DEBUG
            parameters.CharacterId = "ACTOR_PLAYER_DEBUGGER";
#endif

            // Set up the basic parameters
            var levelParameters = new LevelGenerationParameters { LevelType = LevelType.Tutorial };
            var player = CreationService.CreatePlayer(parameters.CharacterId);
            var levelData = _levelService.GenerateLevel(levelParameters, player);

            var response = new GameResponse
            {
                UID = Guid.NewGuid(),
                State = new GameState
                {
                    NumMoves = 0,
                    UID = Guid.NewGuid()
                }
            };

            // Ensure line of sight is calculated
            var context = new CommandContext(levelData, this, _entityProvider, _combatManager, _lootProvider, _randomization);
            context.CalculateLineOfSight(player);

            // Set the level into the response now that the LoS has been calculated
            response.State.Level = levelData.BuildLevelDto();
            response.Effects = BuildEffects(context).ToList();
            
            return response;
        }

        private static IEnumerable<EffectDto> BuildEffects(ICommandContext context) => 
            context.Effects.Select(effect => effect.BuildDto());

        /// <inheritdoc />
        public GameResponse HandleGameMove(GameMove move)
        {
            var response = new GameResponse
            {
                State = move.State,
                UID = Guid.NewGuid()
            };

            response.State.NumMoves++;

            var level = response.State.Level.BuildLevelData();

            var context = new CommandContext(level, this, _entityProvider, _combatManager, _lootProvider, _randomization);

            var brainProvider = new PlayerMoveBrainProvider(_aiService, move.Command);
            var keepGoing = _simulator.SimulateGameTurn(context, brainProvider, _simManager);

            // Ensure that vision is accurate for the player before sending back to the client
            context.CalculateLineOfSight(context.Player);

            // Copy over collections
            response.Effects = BuildEffects(context).ToList();
            response.Messages = context.Messages.ToList();

            if (!keepGoing)
            {
                EndGame(response.State, context.Level.Id == LevelType.Escaped, context);
            }

            // Round-trip the level data back into a new DTO
            response.State.Level = context.Level.ToDto();
            
            return response;
        }

        public void HandleCellMoveCommand(ICommandContext context, IActor actor, Pos2D targetPos)
        {
            _simulator.HandleCellMoveCommand(context, actor, targetPos);
        }

        private static void EndGame(GameState state, bool isVictory, ICommandContext context)
        {
            state.IsGameOver = true;
            state.IsVictory = isVictory;
            state.Score = CalculateScore(state, context);
        }

        private static int CalculateScore(GameState state, ICommandContext context)
        {
            if (state.NumMoves < 1)
            {
                state.NumMoves = 1;
            }

            // Give credit for doing more damage than you received
            state.Score = (context.Player.DamageDealt - context.Player.DamageReceived) * 5;

            // Give credit per kill, but try to encourage the user to go quickly
            state.Score += (int)Math.Round(context.Player.KillCount / (state.NumMoves * 100m));

            // Also give credit for staying alive
            state.Score += state.NumMoves;

            // Winning is bonus points for sure
            state.Score += ((int)context.Level.Id - 1) * 1000;

            return state.Score;
        }

        public void MoveToLevel(LevelType nextLevelType, ICommandContext commandContext)
        {
            var levelParams = new LevelGenerationParameters
            {
                LevelType = nextLevelType,
                PlayerId = commandContext.Player.ObjectId
            };

            commandContext.Player.ClearKnownCells();

            var nextLevel = _levelService.GenerateLevel(levelParams, commandContext.Player);
            commandContext.SetLevel(nextLevel);
        }

        public void HandleActorCommand(ICommandContext context, IActor actor, string commandId, Pos2D commandPosition)
        {
            var brain = _aiService.GetBrainForActor(actor);

            brain?.HandleActorCommand(context, actor, commandId, commandPosition);
        }
    }
}