using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class SpawnEffect : EffectBase
    {

        public SpawnEffect(GameObjectBase source) : base(source)
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