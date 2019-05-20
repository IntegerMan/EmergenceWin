using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Entities
{
    public class HelpTile : GameObjectBase
    {
        public HelpTile(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInvulnerable => false; // Ya know what? If folks hate 'em? Kill 'em.
        public override bool IsTargetable => true;
        public override bool IsInteractive => true;

        protected override string CustomName => "Help Provider";

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
        {
            if (!actor.IsPlayer)
            {
                return false;
            }

            context.DisplayHelp(this, ObjectId);

            return false;
        }
    }
}