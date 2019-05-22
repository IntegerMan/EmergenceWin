using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(CommandContext context, Actor executor, Pos2D pos)
        {
            var strength = context.Randomizer.GetInt(2, 4);

            if (executor.IsPlayer || context.CanPlayerSee(executor.Pos))
            {
                context.AddMessage($"{executor.Name} overloads", ClientMessageType.Generic);
            }

            context.CombatManager.HandleExplosion(context, executor, executor.Pos, strength, 4, DamageType.Normal);

        }

    }
}