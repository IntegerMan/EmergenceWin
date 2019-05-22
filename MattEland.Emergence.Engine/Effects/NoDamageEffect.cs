using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class NoDamageEffect : EffectBase
    {
        public NoDamageEffect(GameObjectBase source) : base(source)
        {
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.NoDamage,
                Text = "0",
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
            };
        }        
    }
}