﻿using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities
{
    public class MaxOperationsPickup : GameObjectBase
    {
        public MaxOperationsPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';
        protected override string CustomName => $"Max Operations +{Potency}";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                actor.MaxOperations += Potency;
                actor.Operations += Potency;

                context.AddEffect(new HelpTextEffect(this, $"Max Operations +{Potency}"));
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 1;

        public override int ZIndex => 10;

    }
}