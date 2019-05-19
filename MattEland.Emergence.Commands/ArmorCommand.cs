using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class ArmorCommand : GameCommand
    {
        public override string Id => "armor";
        public override string Name => "Armor";
        public override string Description => "Increases your damage resistance while active.";
        public override int ActivationCost => 1;
        public override string IconId => "beenhere";

        public override Rarity Rarity => Rarity.Epic;

        public override string ShortName => "ARMOR";
        public override CommandActivationType ActivationType => CommandActivationType.Active;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // This is handled pre-action
        }

        public override void ApplyPreActionEffect(ICommandContext context, IActor executor, Pos2D playerPosition)
        {
            executor.EffectiveDefense += 1;
        }

        protected override void OnActivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveDefense += 1;
        }

        protected override void OnDeactivated(ICommandContext context, IActor executor, Pos2D pos)
        {
            executor.EffectiveDefense -= 1;
        }
    }
}