using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class TeleportEffect : EffectBase
    {
        private readonly Pos2D _startPos;
        private readonly Pos2D _endPos;

        public TeleportEffect(Pos2D startPos, Pos2D endPos) : base(null)
        {
            _startPos = startPos;
            _endPos = endPos;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.Teleport,
                StartPos = _startPos.SerializedValue,
                EndPos = _endPos.SerializedValue,
            };
        }        
    }
}