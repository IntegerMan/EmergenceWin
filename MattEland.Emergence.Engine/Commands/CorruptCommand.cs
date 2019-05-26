using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    [UsedImplicitly]
    public class CorruptCommand : GameCommand
    {
        public override string Id => "corrupt";
        public override string Name => "Corrupt";
        public override string ShortName => "CORRUPT";
        public override string Description => "An active command that corrupts nearby objects";
        public override int ActivationCost => 1;
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override Rarity Rarity => Rarity.Rare;

        public override string IconId => "gradient";

        protected override void OnActivated(CommandContext context, Actor executor, Pos2D pos)
        {
            CorruptionHelper.CorruptNearby(pos, context, executor);
        }

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            CorruptionHelper.CorruptNearby(pos, context, executor);
        }

    }
}