using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

namespace MattEland.Emergence.Definitions.Entities
{
    public class DataStore : GameObjectBase
    {
        public DataStore(GameObjectDto dto) : base(dto)
        {
        }

        protected override string CustomName => "Data Store";

        public override bool IsInteractive => true;
        public override char AsciiChar => 'd';
        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayText($"The {Name} does not respond to your queries.", ClientMessageType.Generic);
        }

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
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