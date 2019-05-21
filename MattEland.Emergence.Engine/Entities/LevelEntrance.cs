﻿using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
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
        public override bool OnActorAttemptedEnter(CommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage("You can't turn back; The network topology doesn't allow for it.", ClientMessageType.Failure);
            }

            return false;
        }

        public override string ForegroundColor => GameColors.Yellow;

        public override bool IsCorruptable => false;
    }
}