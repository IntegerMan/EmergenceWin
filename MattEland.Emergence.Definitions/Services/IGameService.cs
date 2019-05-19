using GeneticSharp.Domain.Randomizations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface IGameService
    {
        /// <summary>
        /// Starts a new game and returns a game response indicating the game's initial state.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A response including the game's initial state.</returns>
        GameResponse StartNewGame(NewGameParameters parameters);

        /// <summary>
        /// Handles a game move and spits out a response for the client to interpret.
        /// </summary>
        /// <param name="move">The move.</param>
        /// <returns>The response state.</returns>
        GameResponse HandleGameMove(GameMove move);

        /// <summary>
        /// Handles a move for an <paramref name="actor"/> to a new  <paramref name="targetPos"/>.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="actor">The actor executing the move.</param>
        /// <param name="targetPos">The tile to move into or interact with / attack</param>
        void HandleCellMoveCommand(ICommandContext context, IActor actor, Pos2D targetPos);
        void MoveToLevel(LevelType nextLevelType, ICommandContext commandContext);
        
        /// <summary>
        /// Executes a specific command for a given actor
        /// </summary>
        /// <param name="context">The command context</param>
        /// <param name="actor">The actor executing the command</param>
        /// <param name="commandId">The command to invoke</param>
        /// <param name="commandPosition">The cell targeted by the command (or the actor's current cell)</param>
        void HandleActorCommand(ICommandContext context,
                                                IActor actor,
                                                string commandId,
                                                Pos2D commandPosition);

        IArtificialIntelligenceService AIService { get; }
        ILootProvider LootProvider { get; }
        ICombatManager CombatManager { get; }
        IEntityDefinitionService EntityProvider { get; }
        ISimulationManager SimManager { get; }
        IRandomization Randomization { get; }
    }
}