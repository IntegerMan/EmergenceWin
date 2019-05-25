using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Effects
{
    public class TauntEffect : EffectBase
    {
        public string Text { get; }

        public TauntEffect(GameObjectBase source, [NotNull] string text) : base(source)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

    }
}