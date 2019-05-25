﻿using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Effects
{
    public class MissedEffect : EffectBase
    {
        public MissedEffect([NotNull] GameObjectBase source) : base(source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
        }
    }
}