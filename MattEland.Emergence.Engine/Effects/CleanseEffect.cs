using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
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