using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Door : OpenableGameObjectBase
    {
        public Door(OpenableDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => IsOpen ? '.' : '+';

        public override string ForegroundColor => GameColors.Yellow;

        /*
        public override void MaintainActiveEffects(CommandContext context)
        {
            base.MaintainActiveEffects(context);

            // Auto-open / auto-close based on whether actors are nearby
            var cells = context.Level.GetCellsInSquare(Pos, 1);
            var trigger = (cells.Where(c => c.Actor != null).Select(c => c.Actor).FirstOrDefault(ShouldOpenFor));
            var detected = trigger != null;

            if (IsOpen != detected)
            {
                IsOpen = detected;
                if (detected)
                {
                    OnOpened(context, trigger);
                }
                context.UpdateObject(this);
            }

        }
        */

        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (IsOpen)
            {
                context.MoveObject(actor, Pos);
                return true;
            }

            if (ShouldOpenFor(actor))
            {
                IsOpen = true;
                context.UpdateObject(this);
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

            if (actor.Team == Alignment.Bug || actor.Team == Alignment.Virus)
            {
                return true;
            }

            return false;
        }

        public override bool BlocksSight => !IsOpen;

        protected override string CustomName => IsOpen ? "Open Access Port" : "Closed Access Port";
    }
}