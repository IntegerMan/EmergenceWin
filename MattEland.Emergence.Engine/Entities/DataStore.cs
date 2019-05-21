﻿using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class DataStore : GameObjectBase
    {
        public DataStore(GameObjectDto dto) : base(dto)
        {
        }

        protected override string CustomName => "Data Store";

        public override bool IsInteractive => true;
        public override char AsciiChar => 'd';

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                if (IsCorrupted)
                {
                    context.AddMessage($"The {Name} is corrupt and cannot be accessed.", ClientMessageType.Failure);
                }
                else
                {
                    context.AddMessage($"The {Name} does not respond to your queries.", ClientMessageType.Generic);
                }
            }

            return false;
        }

        public override string ForegroundColor => GameColors.Purple;

    }
}