using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
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

        public override string Name => "Outgoing Port";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
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