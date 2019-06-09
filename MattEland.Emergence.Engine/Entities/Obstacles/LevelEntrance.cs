using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class LevelEntrance : GameObjectBase
    {
        public LevelEntrance(Pos2D pos) : base(pos)
        {
        }

        public override GameObjectType ObjectType => GameObjectType.Entrance;
        public override bool IsInvulnerable => true;
        public override char AsciiChar => '>';
        public override bool IsTargetable => true;

        public override string Name => "Incoming Port";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (actor.IsPlayer)
            {
                context.AddMessage("You can't turn back; The network topology doesn't allow for it.", ClientMessageType.Failure);
            }

            return false;
        }

        public override string ForegroundColor => GameColors.Yellow;

        public override bool IsCorruptable => false;
    }
}