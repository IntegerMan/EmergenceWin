using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
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
                StartPos = Source.Pos.SerializedValue,
                EndPos = Source.Pos.SerializedValue,
            };
        }        
    }
}