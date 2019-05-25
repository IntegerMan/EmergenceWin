using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        protected override void OnActivated(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveLineOfSightRadius += 2;
        }

        protected override void OnDeactivated(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveLineOfSightRadius -= 2;
        }

        public override void ApplyPreActionEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveLineOfSightRadius += 2;
        }
    }
}