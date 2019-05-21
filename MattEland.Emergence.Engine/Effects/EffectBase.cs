using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public abstract class EffectBase
    {
        protected EffectBase(IGameObject source)
        {
            Source = source;
        }

        public IGameObject Source { get; set; }

        public abstract EffectDto BuildDto();
    }
}