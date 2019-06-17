using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Effects
{
    public class HelpTextEffect : EffectBase
    {
        [NotNull] public string HelpText { get; }

        public HelpTextEffect(GameObjectBase source, [NotNull] string helpText) : base(source)
        {
            HelpText = helpText ?? throw new ArgumentNullException(nameof(helpText));
        }

        public override string ToString() => $"Help: {HelpText}";

        public override string ForegroundColor => GameColors.Blue;
    }
}