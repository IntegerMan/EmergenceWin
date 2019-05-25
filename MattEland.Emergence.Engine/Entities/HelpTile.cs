using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Entities
{
    public class HelpTile : GameObjectBase
    {
        public HelpTile(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => false; // Ya know what? If folks hate 'em? Kill 'em.
        public override bool IsTargetable => true;
        public override bool IsInteractive => true;
        public override char AsciiChar => '?';

        public override string Name => "Help Provider";

        public override bool OnActorAttemptedEnter(CommandContext context, Actor actor)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            context.DisplayHelp(this, ObjectId);

            return false;
        }

        public override string ForegroundColor => GameColors.White;
        public override string BackgroundColor => GameColors.DarkBlue;
    }
}