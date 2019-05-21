using System.Linq;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Door : OpenableGameObjectBase
    {
        public Door(OpenableDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => IsOpen ? '.' : '+';
        public override void OnInteract(CommandContext context, IActor actor)
        {
            if (!IsOpen)
            {
                IsOpen = true;
                context.UpdateObject(this);
            }
            else
            {
                context.MoveObject(actor, Pos);
            }
        }

        public override string ForegroundColor => GameColors.Yellow;

        public override void MaintainActiveEffects(ICommandContext context)
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
            }

        }

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            if (ShouldOpenFor(actor) || IsOpen)
            {
                return base.OnActorAttemptedEnter(context, actor);
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