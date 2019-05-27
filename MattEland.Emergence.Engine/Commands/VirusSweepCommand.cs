using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    [UsedImplicitly]
    public class VirusSweepCommand : GameCommand
    {
        public override string Id => "sweep";
        public override string Name => "Sweep";
        public override string ShortName => "SWEEP";
        public override string Description => "An active command that reduces nearby corruption when on";
        public override int ActivationCost => 1;
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override Rarity Rarity => Rarity.Legendary;

        public override string IconId => "select_all";

        protected override void OnActivated(CommandContext context, Actor executor, Pos2D pos)
        {
            CorruptionHelper.CleanseNearby(context, executor, pos);
        }

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            CorruptionHelper.CleanseNearby(context, executor, pos);
        }

    }
}