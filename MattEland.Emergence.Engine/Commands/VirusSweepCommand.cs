using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Effects;
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
                var isCellVisible = context.CanPlayerSee(cell.Pos);

                // Add the effect for the cell
                if (isCellVisible && cell.Corruption > 0)
                {
                    context.AddEffect(new CleanseEffect(cell.Pos, strength));
                }

                // Reduce base corruption
                cell.Corruption -= strength;

                // Also cleanse any objects on the cell
                foreach (var obj in cell.Objects.Where(o => o.IsCorruptable || (o.Team == Alignment.Bug || o.Team == Alignment.Virus)).ToList())
                {
                    obj.ApplyCorruptionDamage(context, executor, -strength);
                }

            }
        }
    }
}