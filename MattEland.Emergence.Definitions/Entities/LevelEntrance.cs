﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

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
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage("You can't turn back; The network topology doesn't allow for it.", ClientMessageType.Failure);
            }

            return false;
        }

        public override string ForegroundColor => GameColors.Yellow;

        public override bool IsCorruptable => false;
        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayText("You can't turn back; The network topology doesn't allow for it.", ClientMessageType.Failure);
        }
    }
}