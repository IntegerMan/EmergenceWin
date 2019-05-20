﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class LevelService : GameObjectBase
    {
        public LevelService(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => '*';
        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayText($"The {Name} spins and whirs, oblivious to your concerns.");
        }

        protected override string CustomName => "Service";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                if (IsCorrupted)
                {
                    context.AddMessage($"The {Name} has been corrupted and spins chaotically.", ClientMessageType.Failure);
                }
                else
                {
                    context.AddMessage($"The {Name} spins and whirs, oblivious to your concerns.",
                                       ClientMessageType.Generic);
                }
            }

            return false;
        }

        public override string ForegroundColor => GameColors.Orange;

    }
}