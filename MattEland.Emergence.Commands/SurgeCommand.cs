using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class SurgeCommand : GameCommand
    {
        public override string Id => "surge";
        public override string Name => "Surge";
        public override string ShortName => "SURGE";
        public override string Description =>
            "Triggers a surge at the target location, causing damage to the stability of nearby processes.";

        public override int ActivationCost => 4;

        public override Rarity Rarity => Rarity.Epic;

        public override string IconId => "flash_on";

        public override CommandActivationType ActivationType => CommandActivationType.Targeted;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            var strength = context.Randomizer.GetInt(2, 4);

            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(pos))
            {
                context.AddEffect(new ProjectileEffect(executor, pos));

                context.AddMessage($"{executor.Name} creates a power surge", ClientMessageType.Generic);
            }

            context.CombatManager.HandleExplosion(context, executor, pos, strength, 3, DamageType.Normal);

        }
    }
}