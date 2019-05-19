using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class TargetingCommand : GameCommand
    {
        public override string Id => "targeting";
        public override string Name => "Targeting";
        public override string Description => "Increases your accuracy and makes you less likely to miss.";
        public override int ActivationCost => 1;
        public override string IconId => "gps_fixed";

        public override string ShortName => "TARGET";
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override Rarity Rarity => Rarity.Rare;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        public override void ApplyPreActionEffect(ICommandContext context, IActor executor, Pos2D playerPosition)
        {
            executor.EffectiveAccuracy += 1;
        }

        protected override void OnActivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveAccuracy += 1;
        }

        protected override void OnDeactivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveAccuracy -= 1;
        }
    }
}