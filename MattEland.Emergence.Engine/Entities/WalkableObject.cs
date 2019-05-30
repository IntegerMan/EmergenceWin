using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Entities
{
    public abstract class WalkableObject : GameObjectBase
    {
        protected WalkableObject(GameObjectDto dto) : base(dto)
        {
        }

        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            context.MoveObject(actor, Pos);

            return true;
        }

        public override int ZIndex => 0;
    }
}