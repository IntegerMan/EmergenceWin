using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Effects
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