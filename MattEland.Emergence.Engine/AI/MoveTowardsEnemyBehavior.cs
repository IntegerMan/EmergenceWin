using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.AI
{
    /**
     * A simple behavior that moves the actor towards the nearest target it can see that it is hostile to.
     */
    public class MoveTowardsEnemyBehavior : ActorBehaviorBase
    {
        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            var target = choices.Where(c => actor.IsHostileTo(c.Actor)).OrderBy(c => c.Pos.CalculateDistanceFrom(actor.Pos)).FirstOrDefault();

            if (target == null) return false;
            
            // TODO: PathTo the position using a*

            Pos2D newPos;
            if (target.Pos.X < actor.Pos.X && !HasObstacle(context, actor.Pos, MoveDirection.Left))
            {
                newPos = actor.Pos.GetNeighbor(MoveDirection.Left);
            } 
            else if (target.Pos.X > actor.Pos.X && !HasObstacle(context, actor.Pos, MoveDirection.Right))
            {
                newPos = actor.Pos.GetNeighbor(MoveDirection.Right);
            } 
            else if (target.Pos.Y < actor.Pos.Y && !HasObstacle(context, actor.Pos, MoveDirection.Up))
            {
                newPos = actor.Pos.GetNeighbor(MoveDirection.Up);
            } 
            else if (target.Pos.Y > actor.Pos.Y && !HasObstacle(context, actor.Pos, MoveDirection.Down))
            {
                newPos = actor.Pos.GetNeighbor(MoveDirection.Down);
            }
            else
            {
                return false;
            }

            context.MoveObject(actor, newPos);
            return true;
        }

        private bool HasObstacle(GameContext context, Pos2D actorPos, MoveDirection direction)
        {
            var cell = context.Level.GetCell(actorPos.GetNeighbor(direction));
            
            return cell == null || cell.HasObstacle;
        }
    }
}