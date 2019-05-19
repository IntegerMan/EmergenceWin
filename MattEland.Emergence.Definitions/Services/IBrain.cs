using System.Collections.Generic;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Services
{
    public interface IBrain
    {
        string Id { get; }

        /// <summary>
        /// Gets the actor's choice for which tile to attempt to move to or attack.
        /// </summary>
        /// <param name="actor">The actor making the decision.</param>
        /// <param name="choices">The available cell choices.</param>
        /// <param name="visible">The visible cells.</param>
        /// <returns>The actor's choice.</returns>
        IGameCell GetActorChoice(IActor actor, IList<IGameCell> choices, IList<IGameCell> visible, ICommandContext context);

        bool HandleSpecialCommand(ICommandContext context, IActor actor);

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

        void UpdateActorState(ICommandContext context, IActor actor, IGameCell choice);
    }
}