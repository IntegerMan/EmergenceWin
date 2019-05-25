using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public abstract class OpenableGameObjectBase : GameObjectBase
    {
        public bool IsOpen { get; set; }

        protected OpenableGameObjectBase(OpenableDto dto) : base(dto)
        {
            IsOpen = dto.IsOpen;
        }

        public override bool IsInteractive => true;

        /// <inheritdoc />
        protected override GameObjectDto CreateDto()
        {
            return new OpenableDto();
        }

        /// <inheritdoc />
        protected override void ConfigureDto(GameObjectDto dto)
        {
            base.ConfigureDto(dto);

            var openableDto = (OpenableDto) dto;

            openableDto.IsOpen = IsOpen;
        }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (!IsOpen)
            {
                context.AddEffect(new SoundEffect(this, SoundEffects.DoorOpened));
                IsOpen = true;

                OnOpened(context, actor);

                // We want the open object action to count as the move
                return false;
            }

            return true;
        }

        /// <summary>
        /// Called when the object is opened.
        /// </summary>
        protected virtual void OnOpened(CommandContext context, GameObjectBase opener)
        {
            
        }
    }
}