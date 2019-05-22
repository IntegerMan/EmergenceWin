using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class ProjectileEffect : EffectBase
    {
        private readonly Pos2D _endPos;

        public ProjectileEffect(GameObjectBase source, Pos2D endPos) : base(source)
        {
            _endPos = endPos;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Projectile,
                StartPos = Source?.Pos.SerializedValue,
                EndPos = _endPos.SerializedValue,
            };
        }        
    }
}