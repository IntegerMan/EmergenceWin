using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using System.Linq;
using MattEland.Emergence.Helpers;

namespace MattEland.Emergence.Commands
{
    public class EscapeCommand : GameCommand
    {
        public override string Id => "escape";
        public override string Name => "Escape";

        public override string ShortName => "ESCAPE";

        public override string Description =>
            "Teleports the executing process at random without any knowledge of the destination.";

        public override Rarity Rarity => Rarity.Legendary;

        public override string IconId => "keyboard_tab";

        public override int ActivationCost => 2;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            IGameCell target = context.Level.Cells.Where(c => !c.HasObstacle).GetRandomElement(context.Randomizer);

            // This should rarely ever happen, but is potentially possible
            if (target == null)
            {
                if (executor.IsPlayer || context.CanPlayerSee(executor.Position))
                {
                    context.AddMessage($"{executor.Name} tried to escape, but no target location could be found.",
                                       ClientMessageType.Failure);
                }

                return;
            }

            if (executor.IsPlayer || context.CanPlayerSee(executor.Position))
            {
                context.AddMessage($"{executor.Name} escapes.", ClientMessageType.Generic);
            }

            context.TeleportActor(executor, target.Pos);

        }
    }
}