using System.Collections.Generic;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.AI
{
    public class IdleBehavior : ActorBehaviorBase
    {
        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            // Do nothing by design

            return true; // This will always return true as it is the last link in the chain
        }
    }
}