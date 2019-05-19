using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.EntityLogic
{
    public class Firewall : GameObjectBase
    {
        public Firewall(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => true;
        public override bool IsInteractive => true;

        protected override string CustomName => "Firewall";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            // Some actors can always breach the firewall
            if (actor.Team == Alignment.SystemCore || actor.Team == Alignment.SystemSecurity || actor.Team == Alignment.SystemAntiVirus)
            {
                return true;
            }

            if (actor.IsPlayer)
            {
                // Let the player in if they have admin access for the level
                if (context.Level.HasAdminAccess || actor.ObjectId == "ACTOR_PLAYER_DEBUGGER")
                {
                    return true;
                }

                context.AddMessage(
                    "You must capture all cores on the system before you can breach the firewall.",
                    ClientMessageType.Failure);
            }

            return false;
        }

        public override bool IsCorruptable => false;
    }
}