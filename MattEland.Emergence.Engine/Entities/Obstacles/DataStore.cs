using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class DataStore : GameObjectBase
    {
        public DataStore(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Data Store";

        public override char AsciiChar => 'd';

        public override GameObjectType ObjectType => GameObjectType.DataStore;

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
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