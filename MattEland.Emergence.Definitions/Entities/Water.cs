using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
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

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage("You can't do that; nobody implemented swimming!", ClientMessageType.Failure);
            }

            return false;
        }
    }
}