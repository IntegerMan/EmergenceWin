using JetBrains.Annotations;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            if (executor.AdjustStability(1) && (executor.IsPlayer || context.CanPlayerSee(executor.Pos)))
            {
                context.AddEffect(new StabilityRestoreEffect(executor, 1));
            }
        }
    }
}