using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
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
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor)
        {
            if (!IsOpen)
            {
                // TODO: It'd be nice to queueueue up a sound effect here for this.
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
        protected virtual void OnOpened(ICommandContext context, IGameObject opener)
        {
            
        }
    }
}