﻿using JetBrains.Annotations;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Commands
{
    [UsedImplicitly]
    public class RecallCommand : GameCommand
    {
        public override string Id => "recall";
        public override string Name => "Recall";
        public override string ShortName => "RECALL";

        public override string Description => "Teleports to the previously stored location (set by the MARK command)";
        public override int ActivationCost => 2;

        public override Rarity Rarity => Rarity.Rare;

        public override LevelType? MinLevel => LevelType.SmartFridge;

        public override string IconId => "keyboard_return";

        public override void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos)
        {
            if (executor.IsPlayer || context.CanPlayerSee(executor.Position) || context.CanPlayerSee(context.Level.MarkedPos))
            {
                context.AddMessage($"{executor.Name} recalls to the previously marked position",
                                   ClientMessageType.Success);
            }

            context.TeleportActor(executor, context.Level.MarkedPos);
        }
    }
}