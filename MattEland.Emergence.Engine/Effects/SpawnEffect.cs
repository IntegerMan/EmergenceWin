using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class SpawnEffect : EffectBase
    {

        public SpawnEffect([NotNull] GameObjectBase source) : base(source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
        }
     
        public override string ForegroundColor => GameColors.LightYellow;

    }
}