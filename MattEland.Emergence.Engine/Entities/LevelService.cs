using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class LevelService : GameObjectBase
    {
        public LevelService(Pos2D pos) : base(pos)
        {
        }

        public override char AsciiChar => '*';

        public override string Name => "Service";

        public override GameObjectType ObjectType => GameObjectType.Service;

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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