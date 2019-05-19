using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class CleanseEffect : EffectBase
    {
        private readonly decimal _amount;
        private readonly Pos2D _position;

        public CleanseEffect(Pos2D position, int amount) : base(null)
        {
            _amount = amount;
            _position = position;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Cleanse,
                Text = $"-{_amount} Corruption",
                StartPos = _position.SerializedValue,
                EndPos = _position.SerializedValue
            };
        }        
    }
}