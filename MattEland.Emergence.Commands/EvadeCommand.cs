using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
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

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        public override void ApplyPreActionEffect(ICommandContext context, IActor executor, Pos2D playerPosition)
        {
            executor.EffectiveEvasion += 1;
        }

        protected override void OnActivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveEvasion += 1;
        }

        protected override void OnDeactivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveEvasion -= 1;
        }
    }
}