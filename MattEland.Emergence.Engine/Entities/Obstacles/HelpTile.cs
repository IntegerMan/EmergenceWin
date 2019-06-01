using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities.Obstacles
{
    public class HelpTile : GameObjectBase
    {
        public string Topic { get; }

        public HelpTile(Pos2D pos, string topic) : base(pos)
        {
            Topic = topic;
        }

        public override GameObjectType ObjectType => GameObjectType.Help;

        public override bool IsInvulnerable => false; // Ya know what? If folks hate 'em? Kill 'em.
        public override bool IsTargetable => true;
        public override char AsciiChar => '?';

        public override string Name => "Help Provider";

        public override bool OnActorAttemptedEnter(GameContext context, Actor actor)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            context.DisplayHelp(this, Topic);

            return false;
        }

        public override string ForegroundColor => GameColors.White;
        public override string BackgroundColor => GameColors.DarkBlue;
    }
}