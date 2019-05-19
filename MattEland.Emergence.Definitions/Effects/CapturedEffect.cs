using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class CapturedEffect : EffectBase
    {
        public CapturedEffect(IGameObject source) : base(source)
        {
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Captured,
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue,
                Text = "Captured"
            };
        }
    }
}