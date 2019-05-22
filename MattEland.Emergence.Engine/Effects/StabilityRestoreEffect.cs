using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class StabilityRestoreEffect : EffectBase
    {
        private readonly decimal _amount;

        public StabilityRestoreEffect(GameObjectBase source, decimal amount) : base(source)
        {
            _amount = amount;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.StabilityRestore,
                Text = $"+{_amount} Stability",
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue
            };
        }        
    }
}