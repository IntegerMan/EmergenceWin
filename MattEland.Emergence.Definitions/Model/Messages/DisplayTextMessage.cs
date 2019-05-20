using System;
using JetBrains.Annotations;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class DisplayTextMessage : GameMessage
    {
        [NotNull]
        public string Text { get; }

        public DisplayTextMessage([NotNull] string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public override string ToString() => $"Display '{Text}'";

    }
}