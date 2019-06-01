using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
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

        public override void ApplyEffect(GameContext context, Actor executor, Pos2D pos)
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