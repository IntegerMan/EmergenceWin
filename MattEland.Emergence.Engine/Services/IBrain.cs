using System.Collections.Generic;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Services
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
        GameCell GetActorChoice(Actor actor, IList<GameCell> choices, IList<GameCell> visible, CommandContext context);

        bool HandleSpecialCommand(CommandContext context, Actor actor);

        /// <summary>
        /// Executes a specific command for a given actor
        /// </summary>
        /// <param name="context">The command context</param>
        /// <param name="actor">The actor executing the command</param>
        /// <param name="commandId">The command to invoke</param>
        /// <param name="commandPosition">The cell targeted by the command (or the actor's current cell)</param>
        void HandleActorCommand(CommandContext context, 
            Actor actor, 
            string commandId, 
            Pos2D commandPosition);

        void UpdateActorState(CommandContext context, Actor actor, GameCell choice);
    }
}