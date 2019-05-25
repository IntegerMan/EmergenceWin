using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class Wall : GameObjectBase
    {
        public Wall(GameObjectDto dto) : base(dto)
        {
            
        }

        public override bool IsInvulnerable => Stability >= 1000 || State == "External";
        public override bool IsTargetable => true;

        public override string Name => IsInvulnerable ? "External Partition" : "Partition";

        public override bool BlocksSight => true;
        public override char AsciiChar => '#';

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage($"The {Name} blocks the way.", ClientMessageType.Failure);
            }

            return false;
        }

        public override string ForegroundColor => GameColors.SlateBlue;
        
        public override void OnDestroyed(CommandContext context, GameObjectBase attacker)
        {
            base.OnDestroyed(context, attacker);

            context.Level.GenerateFillerWallsAsNeeded(Pos);
        }
    }
}