using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
{
    public class DeactivatedEffect : EffectBase
    {
        private readonly string _commandName;

        public DeactivatedEffect(IGameObject source, string commandName) : base(source)
        {
            _commandName = commandName;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.ActivationEnd,
                StartPos = Source?.Position.SerializedValue,
                EndPos = Source?.Position.SerializedValue,
                Text = $"{_commandName} Deactivated"
            };
        }
    }
}