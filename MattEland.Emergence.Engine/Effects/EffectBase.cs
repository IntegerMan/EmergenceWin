using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public abstract class EffectBase
    {
        protected EffectBase(GameObjectBase source)
        {
            Source = source;
        }

        public GameObjectBase Source { get; set; }

        public abstract EffectDto BuildDto();
    }
}