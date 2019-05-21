using System.Linq;
using JetBrains.Annotations;
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

        public override void ApplyPreActionEffect(CommandContext context, IActor executor, Pos2D pos)
        {
        }

        protected override void OnActivated(CommandContext context, IActor executor, Pos2D pos)
        {
            CleanseNearby(context, executor, pos);
        }

        protected override void OnDeactivated(CommandContext context, IActor executor, Pos2D pos)
        {
        }

        public override void ApplyEffect(CommandContext context, IActor executor, Pos2D pos)
        {
            CleanseNearby(context, executor, pos);
        }

        private static void CleanseNearby(CommandContext context, IActor executor, Pos2D pos)
        {
            const int strength = 1;

            var cells = context.Level.GetCellsInSquare(pos, 1);
            foreach (var cell in cells)
            {
                // Apply base corruption
                cell.Corruption += strength;

                // Also cleanse any objects on the cell
                foreach (var obj in cell.Objects.Where(o => o.IsCorruptable && o != executor).ToList())
                {
                    obj.ApplyCorruptionDamage(context, executor, strength);
                }

            }
        }
    }
}