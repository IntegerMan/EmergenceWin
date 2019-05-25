using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class SpawnEffect : EffectBase
    {

        public SpawnEffect([NotNull] GameObjectBase source) : base(source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
        }
     
    }
}