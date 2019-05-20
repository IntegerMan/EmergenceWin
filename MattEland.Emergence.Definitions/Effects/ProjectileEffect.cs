using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class ProjectileEffect : EffectBase
    {
        private readonly Pos2D _endPos;

        public ProjectileEffect(IGameObject source, Pos2D endPos) : base(source)
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