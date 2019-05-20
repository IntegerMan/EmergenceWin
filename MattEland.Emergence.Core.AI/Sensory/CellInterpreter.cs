using System;
using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.AI.Sensory
{
    public static class CellInterpreter
    {
        public static CellInterpretation Interpret(IGameCell cell, IActor evaluator, IList<IGameCell> allCell, ICommandContext context)
        {
            var ci = new CellInterpretation();

            var values = new Dictionary<CellAspectType, decimal>();
            var conditions = new Dictionary<CellAspectType, Func<IGameCell, bool>>
            {
                [CellAspectType.Actor] = c => c.Actor != null && c.Actor != evaluator,
                [CellAspectType.Ally] = c => c.Actor != null && c.Actor != evaluator && ArtificialIntelligenceService.IsSameTeam(evaluator.Team, c.Actor.Team),
                [CellAspectType.Enemy] = c => c.Actor != null && c.Actor != evaluator && ArtificialIntelligenceService.IsOpposingTeam(evaluator.Team, c.Actor.Team),
                [CellAspectType.Corruption] = c => c.Corruption > 0,
                [CellAspectType.Corruptable] = c => c.Objects.Any(o => o != evaluator && o.IsCorruptable),
                [CellAspectType.Capturable] = c => c.Objects.Any(o => o != evaluator && o.IsCapturable),
                [CellAspectType.Destructable] = c => c.Objects.Any(o => o != evaluator && !o.IsInvulnerable),
                [CellAspectType.Player] = c => c.Actor != null && c.Actor.IsPlayer,
                [CellAspectType.Door] = c => c.Objects.Any(o => o != evaluator && o.ObjectType == GameObjectType.Door),
                [CellAspectType.Core] = c => c.Core != null,
                [CellAspectType.Firewall] = c => c.Objects.Any(o => o != evaluator && o.ObjectType == GameObjectType.Firewall),
                [CellAspectType.Turret] = c => c.Objects.Any(o => o != evaluator && o.ObjectType == GameObjectType.Turret),
                [CellAspectType.HasObstacle] = c => c.HasNonActorObstacle,
                [CellAspectType.Self] = c => c.Pos == evaluator.Pos,
            };

            // Default the values
            foreach (var key in conditions.Keys)
            {
                values[key] = 0;
            }

            // Calculate drop-off from other cells
            foreach (var c in allCell)
            {
                // Figure out the distance from the player's hand
                decimal distance;
                if (c == cell)
                {
                    distance = 0;
                }
                else
                {
                    distance = (decimal) c.Pos.CalculateDistanceFrom(cell.Pos);

                    // Don't evaluate cells that are too far away
                    if (distance >= 10)
                    {
                        continue;
                    }
                }

                foreach (var condition in conditions)
                {
                    if (condition.Value.Invoke(c))
                    {
                        values[condition.Key] += (10 - distance) / 10m;
                    }
                }
            }

            // Add the cell-variable aspects
            foreach (var kvp in values)
            {
                ci.AddAspect(kvp.Key, kvp.Value);
            }

            // Factors that don't change by cell
            ci.AddAspect(CellAspectType.CurrentHealth, evaluator.Stability / (decimal)evaluator.MaxStability);
            ci.AddAspect(CellAspectType.CurrentOps, evaluator.Operations / (decimal)evaluator.MaxStability);
            ci.AddAspect(CellAspectType.RandomFactor, (decimal) context.Randomizer.GetDouble());

            // Factor in cores not controlled by the AI
            var cores = context.Level.Cores.ToList();
            if (cores.Count == 0)
            {
                ci.AddAspect(CellAspectType.CoresControlled, 0);
            }
            else
            {
                ci.AddAspect(CellAspectType.CoresControlled, cores.Count(c => c.Team != Alignment.SystemCore) / (decimal) cores.Count);
            }

            return ci;
        }

    }
}