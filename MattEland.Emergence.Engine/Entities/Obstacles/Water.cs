using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class Water : GameObjectBase
    {
        public Water(Pos2D pos) : base(pos)
        {
        }

        public override GameObjectType ObjectType => GameObjectType.Water;
        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;

        public override char AsciiChar => '~';
        public override bool IsCorruptable => true;

        public override string Name => "Thread Pool";

        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage("You can't do that; nobody implemented swimming!", ClientMessageType.Failure);
            }

            return false;
        }

        public override string ForegroundColor => GameColors.LightBlue;
        public override string BackgroundColor => GameColors.DarkBlue;
    }
}