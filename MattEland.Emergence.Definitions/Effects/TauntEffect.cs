﻿using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class TauntEffect : EffectBase
    {
        private readonly string _text;

        public TauntEffect(IGameObject source, string text) : base(source)
        {
            _text = text;
        }

        public override EffectDto BuildDto()
        {
            EffectType tauntType;
            switch (Source.Team)
            {
                case Alignment.SystemAntiVirus:
                case Alignment.SystemSecurity:
                    tauntType = EffectType.SysSecTaunt;
                    break;
                case Alignment.Bug:
                case Alignment.Virus:
                    tauntType = EffectType.VirusTaunt;
                    break;
                case Alignment.Player:
                    tauntType = EffectType.PlayerTaunt;
                    break;
                default:
                    tauntType = EffectType.SysTaunt;
                    break;
            }

            return new EffectDto {
                Effect = tauntType,
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue,
                Text = _text
            };
        }
    }
}