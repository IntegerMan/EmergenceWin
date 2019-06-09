using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities
{
    public abstract class WalkableObject : GameObjectBase
    {
        protected WalkableObject(Pos2D pos) : base(pos)
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