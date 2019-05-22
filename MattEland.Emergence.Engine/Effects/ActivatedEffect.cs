using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class ActivatedEffect : EffectBase
    {
        private readonly string _commandName;

        public ActivatedEffect(GameObjectBase source, string commandName) : base(source)
        {
            _commandName = commandName;
        }

        public override EffectDto BuildDto()
        {
            return new EffectDto {
                Effect = EffectType.ActivationStart,
                StartPos = Source?.Pos.SerializedValue,
                EndPos = Source?.Pos.SerializedValue,
                Text = $"{_commandName} Activated"
            };
        }
    }
}