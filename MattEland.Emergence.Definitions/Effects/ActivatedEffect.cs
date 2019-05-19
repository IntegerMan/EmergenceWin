using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class ActivatedEffect : EffectBase
    {
        private readonly string _commandName;

        public ActivatedEffect(IGameObject source, string commandName) : base(source)
        {
            _commandName = commandName;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.ActivationStart,
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue,
                Text = $"{_commandName} Activated"
            };
        }
    }
}