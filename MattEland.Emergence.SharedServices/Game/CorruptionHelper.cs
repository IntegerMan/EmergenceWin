using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;
using MattEland.Emergence.Helpers;

namespace MattEland.Emergence.Services.Game
{
    public static class CorruptionHelper
    {
        /// <summary>
        /// Spreads corruption on the cell, if it already contains corruption.
        /// </summary>
        /// <param name="context">The command context for the game</param>
        /// <param name="cell">The cell in question</param>
        public static void SpreadCorruptionOnCell(ICommandContext context, IGameCell cell)
        {
            // Do nothing if there's no corruption already
            if (cell.Corruption <= 0)
            {
                return;
            }

            var farNeighbors = context.Level.GetCellsInSquare(cell.Pos, 2).Where(c => c != cell).ToList();
            var neighbors = context.Level.GetCellsInSquare(cell.Pos, 1).Where(c => c != cell).ToList();

            // If this was already at max corruption and we don't have a huge number of glitches already, spawn a glitch.
            if (cell.Corruption >= 3 && 
                !cell.HasObstacle && 
                farNeighbors.All(n => n.Actor == null))
            {
                SpawnGlitch(context, cell);
            }

            cell.Corruption++;

            // Corrupt associated corruptable objects as well
            foreach (var obj in cell.Objects.Where(o => o.IsCorruptable))
            {
                obj.ApplyCorruptionDamage(context, obj, 1);
            }

            // Spread corruption to the neighboring cells
            foreach (var neighbor in neighbors)
            {
                neighbor.Corruption++;
            }
        }

        private static void SpawnGlitch(ICommandContext context, IGameCell cell)
        {
            var glitch = CreationService.CreateObject("ACTOR_GLITCH", GameObjectType.Actor, cell.Pos);
            context.Level.AddObject(glitch);
            if (context.CanPlayerSee(cell.Pos))
            {
                context.AddEffect(new SpawnEffect(glitch));
            }
        }

        public static void SpreadCorruption(ICommandContext context, int maxCells)
        {
            // Grab random cells from the level
            var cells = new List<IGameCell>(maxCells);
            for (int i = 0; i <= maxCells; i++)
            {
                var cell = context.Level.Cells.GetRandomElement(context.Randomizer);

                if (cell.Corruption > 0 && !cells.Contains(cell))
                {
                    cells.Add(cell);
                }
            }

            // If no corrupted cells were found, carry on.
            if (!cells.Any())
            {
                return;
            }

            // If we have more than we can process, just randomly order them and pick as much as we can handle
            if (cells.Count > maxCells)
            {
                cells = cells.OrderBy(c => context.Randomizer.GetDouble()).Take(maxCells).ToList();
            }

            // Spread corruption to the cells in question
            foreach (var candidate in cells)
            {
                SpreadCorruptionOnCell(context, candidate);
            }

        }
    }
}