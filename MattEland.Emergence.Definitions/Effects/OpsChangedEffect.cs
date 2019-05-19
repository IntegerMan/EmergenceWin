﻿using System.Globalization;
using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class OpsChangedEffect : EffectBase
    {
        private readonly decimal _amount;

        public OpsChangedEffect(IGameObject source, decimal amount) : base(source)
        {
            _amount = amount;
        }

        public override EffectDto BuildDto()
        {
            var text = _amount < 0 ? $"{_amount} Operation" : $"+{_amount} Operation";
            if (_amount != -1 && _amount != 1)
            {
                text += "s";
            }

            return new EffectDto {
                Effect = EffectType.OpsChanged,
                Text = text,
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue,
                Data = _amount.ToString(CultureInfo.InvariantCulture)
            };
        }        
    }
}