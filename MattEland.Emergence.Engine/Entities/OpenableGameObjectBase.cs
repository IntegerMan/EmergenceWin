﻿using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public abstract class OpenableGameObjectBase : GameObjectBase
    {
        public bool IsOpen { get; private set; }

        protected OpenableGameObjectBase(OpenableDto dto) : base(dto)
        {
            IsOpen = dto.IsOpen;
        }

        public override bool IsInteractive => true;

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (!IsOpen)
            {
                context.AddEffect(new SoundEffect(this, SoundEffects.DoorOpened));
                IsOpen = true;

                Open(context, actor);

                // We want the open object action to count as the move
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when the object is opened.
        /// </summary>
        protected virtual void Open(CommandContext context, GameObjectBase opener)
        {
            IsOpen = true;
            context.AddSoundEffect(this, SoundEffects.DoorOpened);
            context.UpdateObject(this);
        }

        /// <summary>
        /// Called when the object is closed.
        /// </summary>
        protected virtual void Close(CommandContext context, GameObjectBase opener)
        {
            IsOpen = false;
            context.AddSoundEffect(this, SoundEffects.DoorClosed);
            context.UpdateObject(this);
        }
    }
}