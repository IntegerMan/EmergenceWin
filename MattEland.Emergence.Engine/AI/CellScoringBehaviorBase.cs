using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.AI
{
    public abstract class CellScoringBehaviorBase : ActorBehaviorBase
    {
        protected abstract IList<GameCell> FilterChoices(GameContext context, Actor actor, IEnumerable<GameCell> choices);

        /// <summary>
        /// Evaluates a cell for desirability
        /// </summary>
        /// <param name="context">The game context</param>
        /// <param name="cell">The cell to evaluate</param>
        /// <returns>Gets a numeric score for the cell under evaluation</returns>
        protected abstract decimal ScoreCell(GameContext context, GameCell cell);

        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            choices = FilterChoices(context, actor, choices);

            // If we don't have enemies or any options, we can't evaluate
            if (!choices.Any()) return false;
            
            // Find the option that's the farthest away from all visible enemies
            var target = choices.OrderByDescending(c => ScoreCell(context, c)).First();
            
            // Move (or wait) and mark as handled
            if (target.Pos != actor.Pos)
            {
                context.MoveObject(actor, target.Pos);
            }

            return true;
        }
    }
}