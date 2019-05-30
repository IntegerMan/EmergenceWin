using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        protected override void OnActivated(GameContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveDefense += 1;
        }

        protected override void OnDeactivated(GameContext context, Actor executor, Pos2D pos)
        {
            executor.EffectiveDefense -= 1;
        }
    }
}