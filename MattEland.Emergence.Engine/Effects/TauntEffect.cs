using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
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
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
                Text = _text
            };
        }
    }
}