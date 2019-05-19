using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class InfectCommand : GameCommand
    {
        public override string Id => "infect";
        public override string Name => "Infect";
        public override string ShortName => "INFECT";
        public override string Description => "Adds corruption to the target.";
        public override int ActivationCost => 2;
        public override CommandActivationType ActivationType => CommandActivationType.Targeted;

        public override Rarity Rarity => Rarity.Epic;

        public override string IconId => "bug_report";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // Do nothing
            if (executor.IsPlayer || context.CanPlayerSee(pos))
            {
                context.AddEffect(new ProjectileEffect(executor, pos));
            }

            context.AddMessage($"{executor.Name} infects a small area.", ClientMessageType.Generic);
            context.CombatManager.HandleExplosion(context, executor, pos, 1, 1, DamageType.Corruption);
        }
    }
}