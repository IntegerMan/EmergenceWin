using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    public class MarkCommand : GameCommand
    {
        public override string Id => "mark";
        public override string Name => "Mark";
        public override string ShortName => "MARK";
        public override string Description =>
            "Marks the process's current location. This position can be teleported to from anywhere on " + 
            "this machine via the RECALL command. Only one location per machine can be Marked at one time.";

        public override Rarity Rarity => Rarity.Rare;

        public override LevelType? MinLevel => LevelType.MessagingServer;

        public override int ActivationCost => 2;

        public override string IconId => "label_outline";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            if (!executor.IsPlayer)
            {
                context.AddError($"{executor.Name} tries to invoke the {Name} command but does not have permission.");
                return;
            }

            context.Level.MarkedPos = executor.Pos;

            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
            {
                context.AddMessage($"{executor.Name} marks their current position.", ClientMessageType.Success);
                context.AddEffect(new CellMarkedEffect(executor.Pos));
            }
        }
    }
}