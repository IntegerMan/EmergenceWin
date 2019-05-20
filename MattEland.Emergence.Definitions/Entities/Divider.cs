using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Divider : GameObjectBase
    {
        public Divider(GameObjectDto dto) : base(dto)
        {
        }

        protected override string CustomName => "Divider";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage($"The {Name} blocks your path", ClientMessageType.Failure);
            }

            return false;
        }

        public override char AsciiChar => 'X';
    }
}