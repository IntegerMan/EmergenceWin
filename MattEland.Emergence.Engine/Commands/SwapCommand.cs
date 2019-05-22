using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(pos))
            {
                context.AddMessage($"{executor.Name} teleports", ClientMessageType.Generic);
            }

            context.TeleportActor(executor, pos);
        }
    }
}