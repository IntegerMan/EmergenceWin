using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class SoundEffect : EffectBase
    {
        public SoundEffect([NotNull] GameObjectBase source, SoundEffects soundType) : base(source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            Sound = soundType;
        }

        public SoundEffects Sound { get; }
    }
}