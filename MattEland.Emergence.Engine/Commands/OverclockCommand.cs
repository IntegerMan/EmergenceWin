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

        protected override void OnActivated(GameContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveStrength += 1;
        }

        protected override void OnDeactivated(GameContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveStrength -= 1;
        }
    }
}