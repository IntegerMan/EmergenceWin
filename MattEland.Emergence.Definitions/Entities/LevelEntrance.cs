using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class LevelEntrance : GameObjectBase
    {
        public LevelEntrance(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsInteractive => true;
        public override char AsciiChar => '>';
        public override bool IsTargetable => true;

        protected override string CustomName => "Incoming Port";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage("You can't turn back; The network topology doesn't allow for it.",
                                   ClientMessageType.Failure);
            }

            return false;
        }

        public override bool IsCorruptable => false;
    }
}