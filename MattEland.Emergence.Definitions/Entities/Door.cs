using System.Linq;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Door : OpenableGameObjectBase
    {
        public Door(OpenableDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;

        public override void MaintainActiveEffects(ICommandContext context)
        {
            base.MaintainActiveEffects(context);

            // Auto-open / auto-close based on whether actors are nearby
            var cells = context.Level.GetCellsInSquare(Position, 1);
            var trigger = (cells.Where(c => c.Actor != null).Select(c => c.Actor).FirstOrDefault(ShouldOpenFor));
            var detected = trigger != null;

            if (IsOpen != detected)
            {
                IsOpen = detected;
                if (detected)
                {
                    OnOpened(context, trigger);
                }
            }

        }

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (ShouldOpenFor(actor) || IsOpen)
            {
                return base.OnActorAttemptedEnter(context, actor, cell);
            }
            
            context.CombatManager.HandleAttack(context, actor, this, "attacks", actor.AttackDamageType);
            return false;
        }

        private bool ShouldOpenFor(IGameObject actor)
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