using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        public override void ApplyPreActionEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveStrength += 1;
        }

        protected override void OnActivated(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveStrength += 1;
        }

        protected override void OnDeactivated(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveStrength -= 1;
        }
    }
}