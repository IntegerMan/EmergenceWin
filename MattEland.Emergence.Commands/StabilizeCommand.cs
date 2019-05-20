using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Effects;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    /// <summary>
    /// A simple command to restore stability to the executing actor.
    /// </summary>
    [UsedImplicitly]
    public class StabilizeCommand : GameCommand
    {
        public int Strength { get; set; } = 5;

        public override string Id => "stabilize";
        public override string Name => "Stabilize";

        public override Rarity Rarity => Rarity.Rare;

        public override string ShortName => "STABLE";

        public override string Description =>
            "Stabilizes the process and instantly restores a large portion of stability, but at a high operational cost.";

        public override int ActivationCost => 5;

        public override string IconId => "build";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            // Figure out how much to add without going over the maximum stability
            int amount = Math.Min(executor.MaxStability - executor.Stability, Strength);

            // Actually add
            executor.Stability += amount;

            // Display the message to the client, rendering things differently if the player did them.
            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
            {
                var messageType = executor.IsPlayer ? ClientMessageType.Success : ClientMessageType.Generic;
                var message = amount == 0
                    ? $"{executor.Name} stabilizes"
                    : $"{executor.Name} stabilizes, restoring {amount} stability";

                context.AddMessage(message, messageType);
                if (amount > 0)
                {
                    context.AddEffect(new StabilityRestoreEffect(executor, amount));
                }

            }
        }
    }
}