using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class SwapCommand : GameCommand
    {
        public override string Id => "swap";
        public override string Name => "Swap";
        public override string ShortName => "SWAP";
        public override string Description => "Swaps your location with whatever is present at the target position.";
        public override int ActivationCost => 1;
        public override CommandActivationType ActivationType => CommandActivationType.Targeted;

        public override Rarity Rarity => Rarity.Uncommon;

        public override string IconId => "swap_horiz";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(pos))
            {
                context.AddMessage($"{executor.Name} teleports", ClientMessageType.Generic);
            }

            context.TeleportActor(executor, pos);
        }
    }
}