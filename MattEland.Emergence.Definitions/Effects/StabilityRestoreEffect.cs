using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class StabilityRestoreEffect : EffectBase
    {
        private readonly decimal _amount;

        public StabilityRestoreEffect(IGameObject source, decimal amount) : base(source)
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