using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model.Messages;

namespace MattEland.Emergence.Engine.Effects
{
    public abstract class EffectBase : GameMessage
    {
        protected EffectBase([CanBeNull] GameObjectBase source)
        {
            Source = source;
        }

        [CanBeNull] public GameObjectBase Source { get; set; }
    }
}