using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class DeactivatedEffect : EffectBase
    {
        public string CommandName { get; }

        public DeactivatedEffect(GameObjectBase source, string commandName) : base(source)
        {
            CommandName = commandName;
        }
    }
}