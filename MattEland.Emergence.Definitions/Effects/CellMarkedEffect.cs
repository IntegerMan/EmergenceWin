using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class CellMarkedEffect : EffectBase
    {
        private readonly Pos2D _position;

        public CellMarkedEffect(Pos2D position) : base(null)
        {
            _position = position;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.CellMarked,
                Text = "Marked",
                StartPos = _position.SerializedValue,
                EndPos = _position.SerializedValue,
            };
        }        
    }
}