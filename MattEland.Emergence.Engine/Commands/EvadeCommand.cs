using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    [UsedImplicitly]
    public class EvadeCommand : GameCommand
    {
        public override string Id => "evade";
        public override string Name => "Evade";
        public override string Description => "Increases your evasion and makes it harder for others to hit you.";
        public override int ActivationCost => 1;
        public override string IconId => "gps_off";

        public override Rarity Rarity => Rarity.Rare;

        public override string ShortName => "EVADE";
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        public override void ApplyPreActionEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveEvasion += 1;
        }

        protected override void OnActivated(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveEvasion += 1;
        }

        protected override void OnDeactivated(CommandContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveEvasion -= 1;
        }
    }
}