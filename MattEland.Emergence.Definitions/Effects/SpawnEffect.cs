using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class SpawnEffect : EffectBase
    {

        public SpawnEffect(IGameObject source) : base(source)
        {
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Spawn,
                StartPos = Source.Position.SerializedValue,
                EndPos = Source.Position.SerializedValue,
            };
        }        
    }
}