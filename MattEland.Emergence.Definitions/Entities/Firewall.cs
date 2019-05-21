using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Firewall : GameObjectBase
    {
        public Firewall(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => true;
        public override bool IsInteractive => true;
        public override char AsciiChar => '|';

        protected override string CustomName => "Firewall";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            // Some actors can always breach the firewall
            if (actor.Team == Alignment.SystemCore || actor.Team == Alignment.SystemSecurity || actor.Team == Alignment.SystemAntiVirus)
            {
                return true;
            }

            if (actor.IsPlayer)
            {
                // Let the player in if they have admin access for the level
                if (context.Level.HasAdminAccess)
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

        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.MoveObject(actor, Pos);
        }

        public override string ForegroundColor => GameColors.Orange;
    }
}