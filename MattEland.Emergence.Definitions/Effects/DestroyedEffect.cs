using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class DestroyedEffect : EffectBase
    {
        public DestroyedEffect(IGameObject source) : base(source)
        {
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Destroyed,
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
                Text = "Destabilized"
            };
        }
    }
}