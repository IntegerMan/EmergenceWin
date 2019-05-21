using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

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
        public override char AsciiChar => '<';

        protected override string CustomName => "Outgoing Port";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            context.AdvanceToNextLevel();

            return false; // Don't allow the context to think that we've moved within the level
        }

        public override bool IsCorruptable => false;

        public override string ForegroundColor => GameColors.Yellow;
    }
}