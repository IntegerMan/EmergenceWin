using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class Wall : GameObjectBase
    {
        public bool IsExternal { get; }

        public Wall(Pos2D pos, bool isExternal) : base(pos)
        {
            IsExternal = isExternal;
        }

        public override GameObjectType ObjectType => GameObjectType.Wall;

        public override bool IsInvulnerable => Stability >= 1000 || IsExternal;
        public override bool IsTargetable => true;

        public override string Name => IsExternal ? "External Partition" : "Partition";

        public override bool BlocksSight => true;
        public override char AsciiChar => '#';

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage($"The {Name} blocks the way.", ClientMessageType.Failure);
            }

            return false;
        }

        public override string ForegroundColor => GameColors.SlateBlue;
        
        public override void OnDestroyed(GameContext context, GameObjectBase attacker)
        {
            base.OnDestroyed(context, attacker);

            context.GenerateFillerWallsAsNeeded(Pos);
        }
    }
}