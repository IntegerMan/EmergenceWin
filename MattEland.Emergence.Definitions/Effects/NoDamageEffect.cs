﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class NoDamageEffect : EffectBase
    {
        public NoDamageEffect(IGameObject source) : base(source)
        {
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.NoDamage,
                Text = "0",
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
            };
        }        
    }
}