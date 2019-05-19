﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.EntityLogic
{
    public class LevelService : GameObjectBase
    {
        public LevelService(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
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
    }
}