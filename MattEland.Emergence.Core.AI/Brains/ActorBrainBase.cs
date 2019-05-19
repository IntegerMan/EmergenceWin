using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using MattEland.Emergence.Helpers;

namespace MattEland.Emergence.AI.Brains
{
    /// <summary>
    /// The base representation of a "brain" used to evaluate the world's current state and get a response from an actor.
    /// </summary>
    public abstract class ActorBrainBase : IBrain
    {
        public abstract string Id { get; }

        /// <summary>
        /// Gets the actor's choice for which tile to attempt to move to or attack.
        /// </summary>
        /// <param name="actor">The actor making the decision.</param>
        /// <param name="choices">The available cell choices.</param>
        /// <param name="visible">The visible cells.</param>
        /// <param name="context">The command context</param>
        /// <returns>The actor's choice.</returns>
        public virtual IGameCell GetActorChoice(IActor actor, IList<IGameCell> choices, IList<IGameCell> visible, ICommandContext context)
        {
            ICollection<IGameCell> bestChoices = null;
            decimal bestScore = -10000;

            foreach (var choice in choices)
            {
                decimal score = CalculateCellScore(choice, actor, visible, context);
                if (bestChoices == null)
                {
                    bestChoices = new List<IGameCell> { choice };
                    bestScore = score;
                }
                else if (score > bestScore)
                {
                    bestChoices.Clear();
                    bestChoices.Add(choice);
                    bestScore = score;
                }
                else if (score == bestScore)
                {
                    bestChoices.Add(choice);
                }
            }

            if (bestChoices != null && bestChoices.Count == 1)
            {
                return bestChoices.First();
            }
            
            return bestChoices?.GetRandomElement(context.Randomizer);
        }

        public virtual void UpdateActorState(ICommandContext context, IActor actor, IGameCell choice)
        {
            // Do nothing by default
            actor.State = null;
        }

        /// <summary>
        /// Calculates an arbitrary score value for a cell with 0 being neutral, negative being bad, and positive being good.
        /// </summary>
        /// <param name="choice">The choice to evaluate.</param>
        /// <param name="actor">Actor the cell is being evaluated for</param>
        /// <param name="otherCells">The other cells visible around this cell</param>
        /// <param name="context">Command context</param>
        /// <returns>The calculated score of the choice.</returns>
        protected abstract decimal CalculateCellScore(IGameCell choice, IActor actor, IEnumerable<IGameCell> otherCells, ICommandContext context);

        public virtual bool HandleSpecialCommand(ICommandContext context, IActor actor)
        {
            return false;
        }

        
        /// <summary>
        /// Executes a specific command for a given actor
        /// </summary>
        /// <param name="context">The command context</param>
        /// <param name="actor">The actor executing the command</param>
        /// <param name="commandId">The command to invoke</param>
        /// <param name="commandPosition">The cell targeted by the command (or the actor's current cell)</param>
        public void HandleActorCommand(ICommandContext context, 
                                       IActor actor, 
                                       string commandId, 
                                       Pos2D commandPosition)
        {
            var command = CreationService.CreateCommand(commandId);

            if (command == null)
            {
                context.AddError($"The command {commandId} could not be found.");
                return;
            }

            bool isCommandActive = 
                command.Execute(context, actor, commandPosition, actor.IsCommandActive(command));

            actor.SetCommandActiveState(command, isCommandActive);
        }

    }
}