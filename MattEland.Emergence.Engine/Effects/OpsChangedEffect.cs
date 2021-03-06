﻿using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class OpsChangedEffect : EffectBase
    {
        public decimal Amount { get; }

        public OpsChangedEffect(GameObjectBase source, decimal amount) : base(source)
        {
            Amount = amount;
        }
     
        public override string ForegroundColor => GameColors.SlateBlue;
        
    }
}