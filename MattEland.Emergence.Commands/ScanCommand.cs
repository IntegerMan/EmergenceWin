using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class ScanCommand : GameCommand
    {
        public override string Id => "scan";
        public override string Name => "Scan";
        public override string Description => "Extends your visible range and helps identify hidden objects.";
        public override int ActivationCost => 1;
        public override string IconId => "perm_scan_wifi";

        public override Rarity Rarity => Rarity.Rare;

        public override string ShortName => "SCAN";
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        protected override void OnActivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveLineOfSightRadius += 2;
        }

        protected override void OnDeactivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveLineOfSightRadius -= 2;
        }

        public override void ApplyPreActionEffect(ICommandContext context, IActor executor, Pos2D playerPosition)
        {
            executor.EffectiveLineOfSightRadius += 2;
        }
    }
}