using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class HelpTextEffect : EffectBase
    {
        private readonly string _helpText;

        public HelpTextEffect(IGameObject source, string helpText) : base(source)
        {
            _helpText = helpText;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.HelpText,
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
                Text = _helpText
            };
        }
    }
}