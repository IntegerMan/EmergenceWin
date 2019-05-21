﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Divider : GameObjectBase
    {
        public Divider(GameObjectDto dto) : base(dto)
        {
        }

        protected override string CustomName => "Divider";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage($"The {Name} blocks your path", ClientMessageType.Failure);
            }

            return false;
        }

        public override char AsciiChar => 'X';

        public override string ForegroundColor => GameColors.Brown;

    }
}