using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class RestoreCommand : GameCommand
    {
        public override string Id => "restore";
        public override string Name => "Restore";

        public override string Description => "Gradually restores stability over time while active.";
        public override int ActivationCost => 1;
        public override string IconId => "autorenew";

        public override Rarity Rarity => Rarity.Epic;

        public override string ShortName => "RESTR";
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            if (executor.AdjustStability(1) && (executor.IsPlayer || context.CanPlayerSee(executor.Position)))
            {
                context.AddEffect(new StabilityRestoreEffect(executor, 1));
            }
        }
    }
}