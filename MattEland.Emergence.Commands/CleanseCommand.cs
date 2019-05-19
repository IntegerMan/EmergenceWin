using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class CleanseCommand : GameCommand
    {
        public override string Id => "cleanse";
        public override string Name => "Cleanse";
        public override string ShortName => "CLEANSE";
        public override string Description => "Reduces corruption in a small area.";
        public override int ActivationCost => 3;
        public override CommandActivationType ActivationType => CommandActivationType.Targeted;

        public override Rarity Rarity => Rarity.Epic;

        public override string IconId => "remove_circle";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {

            if (executor.IsPlayer || context.CanPlayerSee(executor.Position) || context.CanPlayerSee(pos))
            {
                context.AddMessage($"{executor.Name} cleanses an area of corruption.", ClientMessageType.Generic);
                context.AddEffect(new ProjectileEffect(executor, pos));
            }

            const int strength = 1;

            var cells = context.Level.GetCellsInSquare(pos, 2);
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