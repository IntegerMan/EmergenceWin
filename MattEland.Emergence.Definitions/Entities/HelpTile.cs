using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

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
        public override char AsciiChar => '?';
        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayText(ObjectId, ClientMessageType.Help);
        }

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

        public override string ForegroundColor => GameColors.White;
        public override string BackgroundColor => GameColors.DarkBlue;
    }
}