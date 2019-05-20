using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class MissedEffect : EffectBase
    {
        public MissedEffect(IGameObject source) : base(source)
        {
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Missed,
                Text = "Missed",
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
            };
        }        
    }
}