using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class DeactivatedEffect : EffectBase
    {
        private readonly string _commandName;

        public DeactivatedEffect(GameObjectBase source, string commandName) : base(source)
        {
            _commandName = commandName;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.ActivationEnd,
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
                Text = $"{_commandName} Deactivated"
            };
        }
    }
}