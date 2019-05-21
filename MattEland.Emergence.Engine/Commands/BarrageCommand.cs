using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    [UsedImplicitly]
    public class BarrageCommand : GameCommand
    {
        public override string Id => "barrage";
        public override string Name => "Barrage";
        public override string ShortName => "BARRAGE";

        public override LevelType? MinLevel => LevelType.Bastion;

        public override string Description =>
            "Fires projectiles at up to 6 random targets currently in view.";

        public override Rarity Rarity => Rarity.Legendary;


        public override string IconId => "flare";

        public override int ActivationCost => 5;

        public override void ApplyEffect(CommandContext context, IActor executor, Pos2D pos)
        {
            var candidates = new List<IActor>();

            foreach (var cellPos in executor.VisibleCells)
            {
                var cell = context.Level.GetCell(cellPos);

                if (cell?.Actor != null && cell.Actor != executor && cell.Actor.Team != executor.Team)
                {
                    candidates.Add(cell.Actor);
                }
            }

            if (!candidates.Any())
            {
                if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
                {
                    context.AddMessage($"{executor.Name} attempts to {Name} but no targets are visible", ClientMessageType.Failure);
                }

                return;
            }
            
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
            {
                context.AddMessage($"{executor.Name} barrages", ClientMessageType.Generic);
            }

            // Order randomly
            candidates = candidates.OrderBy(c => context.Randomizer.GetDouble()).ToList();

            // Affect up to 6 targets randomly
            var targets = candidates.Take(Math.Min(6, candidates.Count)).ToList();
            foreach (var target in targets)
            {
                if (executor.IsPlayer || context.CanPlayerSee(target.Pos) ||
                    context.CanPlayerSee(executor.Pos))
                {
                    context.AddEffect(new ProjectileEffect(executor, target.Pos));
                }

                context.CombatManager.HandleExplosion(context, executor, target.Pos, 2, 1, DamageType.Normal);                
            }

        }

    }
}