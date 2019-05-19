using System;
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
    public class BurstCommand : GameCommand
    {
        public override string Id => "burst";
        public override string Name => "Burst";
        public override string ShortName => "BURST";
        public override string Description => "Fires a burst of three projectiles at a target.";
        public override int ActivationCost => 3;
        public override CommandActivationType ActivationType => CommandActivationType.Targeted;

        public override Rarity Rarity => Rarity.Epic;

        public override LevelType? MinLevel => LevelType.SmartFridge;

        public override string IconId => "sort";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            var targets = context.Level.GetCellsInSquare(pos, 1).Where(c => executor.CanSee(c.Pos)).ToList();
            targets = targets.OrderBy(o => context.Randomizer.GetDouble()).ToList();

            // This shouldn't happen, but in the case where zero cells are visible, just do nothing in general
            if (!targets.Any()) {
                if (executor.IsPlayer || context.CanPlayerSee(pos) || context.CanPlayerSee(executor.Position))
                {
                    context.AddMessage($"{executor.Name} tries to use {Name} but had a targeting issue", ClientMessageType.Failure);
                }

                return;
            }

            if (executor.IsPlayer || context.CanPlayerSee(pos) || context.CanPlayerSee(executor.Position))
            {
                context.AddMessage($"{executor.Name} fires a {Name}", ClientMessageType.Generic);
            }

            // Pick 3 random targets that are visible around the target cell and fire projectiles at them
            foreach (var target in targets.Take(Math.Max(3, targets.Count)))
            {
                if (executor.IsPlayer || context.CanPlayerSee(target.Pos))
                {
                    context.AddEffect(new ProjectileEffect(executor, target.Pos));
                }
                
                context.CombatManager.HandleExplosion(context, executor, target.Pos, 2, 1, DamageType.Normal);
            }

        }
    }
}