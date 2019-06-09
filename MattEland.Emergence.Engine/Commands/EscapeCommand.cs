using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(GameContext context, Actor executor, Pos2D pos)
        {
            GameCell target = context.Level.Cells.Where(c => !c.HasObstacle).GetRandomElement(context.Randomizer);

            // This should rarely ever happen, but is potentially possible
            if (target == null)
            {
                if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
                {
                    context.AddMessage($"{executor.Name} tried to escape, but no target location could be found.",
                                       ClientMessageType.Failure);
                }

                return;
            }

            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
            {
                context.AddMessage($"{executor.Name} escapes.", ClientMessageType.Generic);
            }

            context.TeleportActor(executor, target.Pos);

        }
    }
}