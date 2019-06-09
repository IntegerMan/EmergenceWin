using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        protected override void OnActivated(GameContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveAccuracy += 1;
        }

        protected override void OnDeactivated(GameContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveAccuracy -= 1;
        }
    }
}