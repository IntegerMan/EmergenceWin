﻿using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class StabilityRestoreEffect : EffectBase
    {
        public decimal Amount { get; }

        public StabilityRestoreEffect(GameObjectBase source, decimal amount) : base(source)
        {
            Amount = amount;
        }
     
    }
}