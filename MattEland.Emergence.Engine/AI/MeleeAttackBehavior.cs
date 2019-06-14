using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.AI
{
    public class MeleeAttackBehavior : ActorBehaviorBase
    {


        public override bool Evaluate(GameContext context, Actor actor, IEnumerable<GameCell> choices)
        {
            var actorChoices = choices.Where(c => c.Actor != null && c.Actor != actor);
            var hostileChoices = actorChoices.Where(c => actor.IsHostileTo(c.Actor));
            var choice = hostileChoices.FirstOrDefault(c => c.Pos.IsAdjacentTo(actor.Pos));

            if (choice == null) return false;
            
            context.CombatManager.HandleAttack(context, actor, choice.Actor, "attacks", actor.AttackDamageType);
            return true;

        }
    }
}