using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class LevelExit : GameObjectBase
    {
        public LevelExit(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => true;
        public override bool IsInteractive => true;

        protected override string CustomName => "Outgoing Port";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            context.AdvanceToNextLevel();

            return false; // Don't allow the context to think that we've moved within the level
        }

        public override bool IsCorruptable => false;
    }
}