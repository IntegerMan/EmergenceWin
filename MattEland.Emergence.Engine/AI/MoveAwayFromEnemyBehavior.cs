using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.AI
{
    /**
     * A simple behavior that moves the actor as far away from the nearest thing that appears to be hostile to it
     */
    public class MoveAwayFromEnemyBehavior : ActorBehaviorBase
    {
        private List<GameCell> _enemies;

        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            // Grab things to be afraid of
            _enemies = choices.Where(c => c.Actor != null && c.Actor.IsHostileTo(actor)).ToList();
            if (!_enemies.Any()) return false;
            
            var adjacent = FilterChoices(context, actor, choices);

            // If we don't have enemies or any options, we can't evaluate
            if (!adjacent.Any()) return false;
            
            // Find the option that's the farthest away from all visible enemies
            var target = adjacent.OrderByDescending(c => ScoreCell(context, c)).First();
            
            // Move (or wait) and mark as handled
            if (target.Pos != actor.Pos)
            {
                context.MoveObject(actor, target.Pos);
            }

            return true;
        }

        private IList<GameCell> FilterChoices(GameContext context, Actor actor, IEnumerable<GameCell> choices) 
            => choices.Where(c => c.Pos.IsAdjacentTo(actor.Pos) && !c.HasObstacle || c.Pos == actor.Pos).ToList();

        /// <summary>
        /// Evaluates a cell for desirability
        /// </summary>
        /// <param name="context">The game context</param>
        /// <param name="cell">The cell to evaluate</param>
        /// <returns>Gets a numeric score for the cell under evaluation</returns>
        private decimal ScoreCell(GameContext context, GameCell cell)
        {
            var score = 0;
            
            // Main criteria is distance from nearest enemy
            _enemies.Each(e => score += e.Pos.CalculateDistanceInMovesFrom(cell.Pos) * 5);

            // Score the cell by its proximity to openness so the actors will avoid boxing themselves into a corner
            score += cell.FilterAdjacentCells(context, cells => cells.Where(c => !c.HasObstacle)).Count();

            return score;
        }
    }
}