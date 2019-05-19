using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class OverloadCommand : GameCommand
    {
        public override string Id => "overload";
        public override string Name => "Overload";
        public override string ShortName => "OVRLOAD";

        public override Rarity Rarity => Rarity.Epic;

        public override string Description =>
            "Overloads the process, sacrificing stability to destabilize nearby processes.";

        public override string IconId => "blur_circular";

        public override int ActivationCost => 3;

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            var strength = context.Randomizer.GetInt(2, 4);

            if (executor.IsPlayer || context.CanPlayerSee(executor.Position))
            {
                context.AddMessage($"{executor.Name} overloads", ClientMessageType.Generic);
            }

            context.CombatManager.HandleExplosion(context, executor, executor.Position, strength, 4, DamageType.Normal);

        }

    }
}