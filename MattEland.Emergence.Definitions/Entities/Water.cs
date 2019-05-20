using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;
using ICommandContext = MattEland.Emergence.Definitions.Services.ICommandContext;

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
        public override void OnInteract(CommandContext context, IActor actor)
        {
            context.DisplayText("You can't do that; nobody implemented swimming!", ClientMessageType.Failure);
        }

        protected override string CustomName => "Thread Pool";

        public override bool OnActorAttemptedEnter(ICommandContext context, IActor actor, IGameCell cell)
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