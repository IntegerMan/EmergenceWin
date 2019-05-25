using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public abstract class EffectBase
    {
        protected EffectBase(GameObjectBase source)
        {
            Source = source;
        }

        public GameObjectBase Source { get; set; }
    }
}