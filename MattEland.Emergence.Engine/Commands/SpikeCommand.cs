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
    public class SpikeCommand : GameCommand
    {
        public override string Id => "spike";
        public override string Name => "Spike";
        public override string ShortName => "SPIKE";
        public override string Description => "Sends a spike to the target, causing damage to the process' stability.";
        public override int ActivationCost => 2;
        public override CommandActivationType ActivationType => CommandActivationType.Targeted;

        public override Rarity Rarity => Rarity.Common;

        public override string IconId => "call_missed_outgoing";

        public override void ApplyEffect(CommandContext context, IActor executor, Pos2D pos)
        {
            // Do nothing
            var targets = context.Level.GetTargetsAtPos(pos).ToList();

            if (executor.IsPlayer || context.CanPlayerSee(pos))
            {
                context.AddEffect(new ProjectileEffect(executor, pos));
            }

            if (targets.Any())
            {
                foreach (var target in targets)
                {
                    context.CombatManager.HandleAttack(context, executor, target, "spikes", DamageType.Normal);                    
                }
            }
            else
            {
                if (executor.IsPlayer || context.CanPlayerSee(executor.Pos) || context.CanPlayerSee(pos))
                {
                    context.AddMessage($"{executor.Name} sends a spike into nothingness.", ClientMessageType.Failure);
                }
            }
        }
    }
}