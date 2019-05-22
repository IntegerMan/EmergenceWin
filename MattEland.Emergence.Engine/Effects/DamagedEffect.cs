using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Effects
{
    public class DamagedEffect : EffectBase
    {
        private readonly int _amount;
        private readonly DamageType _damageType;

        public DamagedEffect(GameObjectBase source, int amount, DamageType damageType) : base(source)
        {
            _amount = amount;
            _damageType = damageType;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Damage,
                Text = _amount.ToString(),
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue
            };
        }        
    }
}