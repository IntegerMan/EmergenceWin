using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class LevelService : GameObjectBase
    {
        public LevelService(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => '*';

        public override string Name => "Service";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                if (IsCorrupted)
                {
                    context.AddMessage($"The {Name} has been corrupted and spins chaotically.", ClientMessageType.Failure);
                }
                else
                {
                    context.AddMessage($"The {Name} spins and whirs, oblivious to your concerns.", ClientMessageType.Generic);
                }
            }

            return false;
        }

        public override string ForegroundColor => GameColors.Orange;

    }
}