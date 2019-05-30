using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.AI
{

    public class WanderBehavior : ActorBehaviorBase
    {

        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            var option = choices.Where(o => !o.HasNonActorObstacle).GetRandomElement(context.Randomizer);
            if (option.Pos == actor.Pos)
            {
                WaitCommand.Execute(context, actor, actor.Pos, false);
            }
            else
            {
                MoveCommand.Execute(context, actor, option.Pos, false);
            }

            return true;
        }
    }
}