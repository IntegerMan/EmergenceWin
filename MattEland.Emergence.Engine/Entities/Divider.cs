using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Divider : GameObjectBase
    {
        public Divider(GameObjectDto dto) : base(dto)
        {
        }

        public override string Name => "Divider";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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