using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities
{
    public abstract class OpenableGameObjectBase : GameObjectBase
    {
        public bool IsOpen { get; private set; }

        protected OpenableGameObjectBase(Pos2D pos, bool isOpen) : base(pos)
        {
            IsOpen = isOpen;
        }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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
        protected virtual void Open(GameContext context, GameObjectBase opener)
        {
            IsOpen = true;
            context.AddSoundEffect(this, SoundEffects.DoorOpened);
            context.UpdateObject(this);
        }

        /// <summary>
        /// Called when the object is closed.
        /// </summary>
        protected virtual void Close(GameContext context, GameObjectBase opener)
        {
            IsOpen = false;
            context.AddSoundEffect(this, SoundEffects.DoorClosed);
            context.UpdateObject(this);
        }
    }
}