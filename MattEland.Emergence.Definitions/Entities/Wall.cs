using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class Wall : GameObjectBase
    {
        public Wall(GameObjectDto dto) : base(dto)
        {
            
        }

        public override bool IsInvulnerable => Stability >= 1000 || State == "External";
        public override bool IsTargetable => true;

        protected override string CustomName => IsInvulnerable ? "External Partition" : "Partition";

        public override bool BlocksSight => true;
        public override char AsciiChar => '#';

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage($"The {Name} blocks the way.", ClientMessageType.Failure);
            }

            return false;
        }

        public override void OnDestroyed(ICommandContext context, IGameObject attacker)
        {
            base.OnDestroyed(context, attacker);

            context.Level.GenerateFillerWallsAsNeeded(Pos);
        }
    }
}