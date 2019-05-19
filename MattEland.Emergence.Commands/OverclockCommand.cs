using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class OverclockCommand : GameCommand
    {
        public override string Id => "overclock";
        public override string Name => "Overclock";
        public override string Description => "Increases the damage you deal while active.";
        public override int ActivationCost => 1;
        public override string IconId => "access_time";

        public override Rarity Rarity => Rarity.Uncommon;

        public override string ShortName => "OVRCLK";
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        public override void ApplyPreActionEffect(ICommandContext context, IActor executor, Pos2D playerPosition)
        {
            executor.EffectiveStrength += 1;
        }

        protected override void OnActivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveStrength += 1;
        }

        protected override void OnDeactivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveStrength -= 1;
        }
    }
}