using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.AI
{
    /**
     * A simple behavior that moves the actor as far away from the nearest thing that appears to be hostile to it
     */
    public class MoveAwayFromEnemyBehavior : ActorBehaviorBase
    {
        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            // Grab things to be afraid of
            var enemies = choices.Where(c => c.Actor != null && c.Actor.IsHostileTo(actor));
            
            // Get open move options plus the current cell as a wait option
            var adjacent = choices.Where(c => (c.Pos.IsAdjacentTo(actor.Pos) && !c.HasObstacle) || c.Pos == actor.Pos);

            // If we don't have enemies or any options, we can't evaluate
            if (!enemies.Any() || !adjacent.Any()) return false;
            
            // Find the option that's the farthest away from all visible enemies
            var target = adjacent.OrderByDescending(c => enemies.Sum(e => e.Pos.CalculateDistanceInMovesFrom(c.Pos))).First();
            
            // Move (or wait) and mark as handled
            if (target.Pos != actor.Pos)
            {
                context.MoveObject(actor, target.Pos);
            }

            return true;
        }
    }
}