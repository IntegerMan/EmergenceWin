using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Water : GameObjectBase
    {
        public Water(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => true;
        public override bool IsTargetable => false;

        public override char AsciiChar => '~';
        public override bool IsCorruptable => true;

        protected override string CustomName => "Thread Pool";

        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
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