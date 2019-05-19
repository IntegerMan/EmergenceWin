using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Effects
{
    public class DamagedEffect : EffectBase
    {
        private readonly int _amount;
        private readonly DamageType _damageType;

        public DamagedEffect(IGameObject source, int amount, DamageType damageType) : base(source)
        {
            _amount = amount;
            _damageType = damageType;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Damage,
                Text = _amount.ToString(),
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue
            };
        }        
    }
}