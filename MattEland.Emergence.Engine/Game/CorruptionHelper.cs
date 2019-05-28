using System;
using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Game
{
    public static class CorruptionHelper
    {
        public static void ApplyCorruptionDamage(CommandContext context,
            GameObjectBase attacker,
            GameObjectBase defender,
            int damage)
        {
            var originalCorruption = defender.Corruption;

            defender.ApplyCorruptionDamage(context, attacker, damage);

            if (attacker is Actor attackingActor)
            {
                attackingActor.DamageDealt += Math.Abs(defender.Corruption - originalCorruption);
            }
        }

        public static void CorruptNearby(this Pos2D pos, CommandContext context, Actor executor)
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


        /// <summary>
        /// Spreads corruption on the cell, if it already contains corruption.
        /// </summary>
        /// <param name="cell">The cell in question</param>
        /// <param name="context">The command context for the game</param>
        public static void SpreadCorruptionOnCell(this GameCell cell, CommandContext context)
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

        private static void SpawnGlitch(CommandContext context, GameCell cell)
        {
            var glitch = GameObjectFactory.CreateObject(Actors.Glitch, GameObjectType.Actor, cell.Pos);
            context.AddObject(glitch);
            if (context.CanPlayerSee(cell.Pos))
            {
                context.AddEffect(new SpawnEffect(glitch));
            }
        }

        public static void SpreadCorruption(CommandContext context, int maxCells)
        {
            // Grab random cells from the level
            var cells = new List<GameCell>(maxCells);
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
                SpreadCorruptionOnCell(candidate, context);
            }

        }

        public static bool IsCorruptionDamageType(this DamageType damageType) 
            => damageType == DamageType.Corruption || damageType == DamageType.Combination;

        public static void CleanseNearby(CommandContext context, Actor executor, Pos2D pos)
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
                foreach (var obj in cell.Objects.Where(o => o.IsCorruptable || o.Team == Alignment.Bug || o.Team == Alignment.Virus).ToList())
                {
                    obj.ApplyCorruptionDamage(context, executor, -strength);
                }

            }
        }
    }
}