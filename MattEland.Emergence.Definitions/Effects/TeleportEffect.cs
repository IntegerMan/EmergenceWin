using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
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