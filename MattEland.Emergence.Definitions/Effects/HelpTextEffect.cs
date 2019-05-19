using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
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
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue,
                Text = _helpText
            };
        }
    }
}