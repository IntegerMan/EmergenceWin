using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class Door : OpenableGameObjectBase
    {
        public Door(Pos2D pos) : base(pos, false)
        {
        }

        public override char AsciiChar => IsOpen ? '.' : '+';

        public override string ForegroundColor => GameColors.Yellow;

        public override void MaintainActiveEffects(GameContext context)
        {
            base.MaintainActiveEffects(context);

            // Auto-open / auto-close based on whether actors are nearby
            var cells = context.Level.GetCellsInSquare(Pos, 1);
            var trigger = cells.Where(c => c.Actor != null).Select(c => c.Actor).FirstOrDefault(ShouldOpenFor);
            var detected = trigger != null;

            if (IsOpen == detected) return;

            if (detected)
            {
                Open(context, trigger);
            }
            else
            {
                Close(context, trigger);
            }

        }

        public override GameObjectType ObjectType => GameObjectType.Door;

        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (IsOpen)
            {
                context.MoveObject(actor, Pos);
                return true;
            }

            if (ShouldOpenFor(actor))
            {
                Open(context, actor);
                return true;
            }

            context.CombatManager.HandleAttack(context, actor, this, "attacks", actor.AttackDamageType);
            return false;
        }

        private bool ShouldOpenFor(GameObjectBase actor)
        {
            if (actor.IsDead)
            {
                return false;
            }

            if (!IsCorrupted)
            {
                return true;
            }

            return actor.Team == Alignment.Bug || actor.Team == Alignment.Virus;
        }

        public override bool BlocksSight => !IsOpen;

        public override string Name => IsOpen ? "Open Access Port" : "Closed Access Port";
    }
}