using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Game
{
    /// <summary>
    /// A service for dealing with high-level game operations such as new games or player moves.
    /// </summary>
    public sealed class GameService
    {
        [NotNull] private readonly LevelGenerationService _levelService;
        [NotNull] private readonly ILootProvider _lootProvider;
        [NotNull] private readonly ICombatManager _combatManager;
        [NotNull] private readonly IEntityDefinitionService _entityProvider;
        [NotNull] private readonly ISimulationManager _simManager;

        private ILevel _level;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="levelService">The level service.</param>
        /// <param name="aiService">The artificial intelligence service.</param>
        /// <param name="combatManager">The combat manager</param>
        public GameService([NotNull] LevelGenerationService levelService, 
                           [NotNull] ICombatManager combatManager,
                           [NotNull] ILootProvider lootProvider,
                           [NotNull] IEntityDefinitionService entityService,
                           [NotNull] ISimulationManager simManager)
        {
            _levelService = levelService ?? throw new ArgumentNullException(nameof(levelService));
            _combatManager = combatManager ?? throw new ArgumentNullException(nameof(combatManager));
            _lootProvider = lootProvider ?? throw new ArgumentNullException(nameof(lootProvider));
            _entityProvider = entityService ?? throw new ArgumentNullException(nameof(entityService));
            _simManager = simManager ?? throw new ArgumentNullException(nameof(simManager));

            GameCreationConfigurator.ConfigureObjectCreation();
        }

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
            var context = new CommandContext(levelData, this, _entityProvider, _combatManager, _lootProvider);
            context.CalculateLineOfSight(player);

            // Set the level into the response now that the LoS has been calculated
            response.State.Level = levelData.BuildLevelDto();
            response.Effects = BuildEffects(context).ToList();
            
            return response;
        }

        private static IEnumerable<EffectDto> BuildEffects(CommandContext context) => 
            context.Effects.Select(effect => effect.BuildDto());

        /// <inheritdoc />
        public CommandContext HandleGameMove(Pos2D targetPos)
        {
            NumMoves++;

            var context = new CommandContext(_level, this, _entityProvider, _combatManager, _lootProvider);

            // Give objects and actors a chance to react to the current game state
            foreach (var obj in context.Level.Objects.ToList())
            {
                obj.ApplyActiveEffects(context);
            }

            // TODO: _simulator.SimulateGameTurn(context, _simManager);

            // Ensure that vision is accurate for the player before sending back to the client
            context.CalculateLineOfSight(context.Player);
            
            return context;
        }

        public int NumMoves { get; set; }

        private static void EndGame(GameState state, bool isVictory, CommandContext context)
        {
            state.IsGameOver = true;
            state.IsVictory = isVictory;
            state.Score = CalculateScore(state, context);
        }

        private static int CalculateScore(GameState state, CommandContext context)
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

        public void MoveToLevel(LevelType nextLevelType, CommandContext commandContext)
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

    }
}