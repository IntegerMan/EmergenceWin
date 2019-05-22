using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Firewall : GameObjectBase
    {
        public Firewall(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => true;
        public override bool IsInteractive => true;
        public override char AsciiChar => IsOpen ? ':' : '|';
        public bool IsOpen { get; private set; }

        public override void MaintainActiveEffects(CommandContext context)
        {
            IsOpen = context.Level.HasAdminAccess;
        }

        protected override string CustomName => "Firewall";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (IsOpen || actor.Team == Alignment.SystemCore || actor.Team == Alignment.SystemSecurity || actor.Team == Alignment.SystemAntiVirus)
            {
                context.MoveObject(actor, Pos);
                return true;
            }

            if (actor.IsPlayer)
            {
                context.AddMessage("You must capture all cores on the system before you can breach the firewall.", ClientMessageType.Failure);
            }

            return false;
        }

        public override bool IsCorruptable => false;

        public override string ForegroundColor => IsOpen ? GameColors.Green : GameColors.Orange;
    }
}