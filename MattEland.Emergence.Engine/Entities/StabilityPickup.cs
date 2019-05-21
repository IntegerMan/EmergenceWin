﻿using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities
{
    public class StabilityPickup : GameObjectBase
    {
        public StabilityPickup(GameObjectDto dto) : base(dto)
        {
        }

        public override bool IsInteractive => true;
        public override char AsciiChar => 'p';

        protected override string CustomName => "Stability Restore";

        /// <inheritdoc />
        public override bool OnActorAttemptedEnter(CommandContext context, IActor actor)
        {
            if (actor.IsPlayer)
            {
                actor.Stability += Potency;
                context.AddEffect(new StabilityRestoreEffect(this, Potency));
                context.Level.RemoveObject(this);
            }

            return true;
        }

        public int Potency { get; set; } = 5;

        public override int ZIndex => 10;
        
    }
}