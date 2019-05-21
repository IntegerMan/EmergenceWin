using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    [UsedImplicitly]
    public class RecallCommand : GameCommand
    {
        public override string Id => "recall";
        public override string Name => "Recall";
        public override string ShortName => "RECALL";

        public override string Description => "Teleports to the previously stored location (set by the MARK command)";
        public override int ActivationCost => 2;

        public override Rarity Rarity => Rarity.Rare;

        public override LevelType? MinLevel => LevelType.SmartFridge;

        public override string IconId => "keyboard_return";

        public override void ApplyEffect(CommandContext context, IActor executor, Pos2D pos)
        {
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(context.Level.MarkedPos))
            {
                context.AddMessage($"{executor.Name} recalls to the previously marked position",
                                   ClientMessageType.Success);
            }

            context.TeleportActor(executor, context.Level.MarkedPos);
        }
    }
}