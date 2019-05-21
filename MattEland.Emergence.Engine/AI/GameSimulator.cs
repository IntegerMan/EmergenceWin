using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.AI
{
    public class GameSimulator
    {
        private readonly IArtificialIntelligenceService _aiService;

        public GameSimulator(IArtificialIntelligenceService aiService)
        {
            _aiService = aiService;
        }

        public bool SimulateGameTurn(ICommandContext context, IBrainProvider brainProvider, ISimulationManager simManager)
        {
            // Give objects and actors a chance to react to the current game state
            foreach (var obj in context.Level.Objects.ToList())
            {
                obj.ApplyActiveEffects(context);
            }

            // Figure out objects that should participate, including the player
            var smartObjects = context.Level.Cells.SelectMany(c => c.Objects).Where(o => o.HasAI).ToList();
            if (context.Player != null && !context.Player.IsDead)
            {
                smartObjects.Insert(0, context.Player);
            }

            // Process each actor's move
            foreach (var obj in smartObjects)
            {
                if (obj is IActor actor)
                {
                    // Ensure last position is accurate for later evaluation
                    actor.RecentPositions.Add(actor.Pos);
                    if (actor.RecentPositions.Count > 5)
                    {
                        actor.RecentPositions.RemoveAt(0);
                    }

                    // Actors can die but don't get removed from the collection
                    if (actor.IsDead)
                    {
                        continue;
                    }

                    // Make sure it has a good picture of the world before it decides on anything
                    context.CalculateLineOfSight(actor);

                    // Decide and execute a move
                    var brain = brainProvider.GetBrainForActor(actor);
                    _aiService.CarryOutMove(context, actor, brain);
                }
            }

            // If the player has now died, short-circuit everything else
            if (simManager.ShouldEndSimulation(context))
            {
                return false;
            }

            // Every actor will gain an operation every turn
            RegenerateOperations(context);

            // Maintain all objects
            foreach (var obj in context.Level.Objects.ToList())
            {
                obj.MaintainActiveEffects(context);
            }

            // Ensure corruption spreads, but do this after active effects take place so we don't get odd sudden effects
            if (simManager.AllowCorruption)
            {
                CorruptionHelper.SpreadCorruption(context, 2);
            }

            return true;
        }

        private static void RegenerateOperations(ICommandContext context)
        {
            foreach (var actor in context.Level.Actors.ToList())
            {
                // Gain operations every turn not spent in corruption
                var cell = context.Level.GetCell(actor.Pos);
                if ((cell != null && cell.Corruption < 2) || actor.Team == Alignment.Bug || actor.Team == Alignment.Virus)
                {
                    actor.AdjustOperationsPoints(1);
                }
            }
        }

        /// <summary>
        /// Handles a move for an <paramref name="actor"/> to a new  <paramref name="targetPos"/>.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="actor">The actor executing the move.</param>
        /// <param name="targetPos">The tile to move into or interact with / attack</param>
        public void HandleCellMoveCommand(ICommandContext context, IActor actor, Pos2D targetPos)
        {
            var cell = context.Level.GetCell(targetPos);

            if (cell == null)
            {
                if (actor.IsPlayer)
                {
                    context.AddMessage("The emptiness of the void prevents your movement", ClientMessageType.Failure);
                }

                return;
            }

            // Treat a move to current cell as a wait
            if (cell.Pos == actor.Pos)
            {
                return;
            }

            // Attempt to interact with each object in the cell in turn, aborting the move if something stops us
            var objects = cell.Objects.ToList();

            // Cycle through objects in the cell
            foreach (var gameObject in objects)
            {
                if (!gameObject.OnActorAttemptedEnter(context, actor))
                {
                    break;
                }
            }
        }

    }
}