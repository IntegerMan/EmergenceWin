using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;

namespace MattEland.Emergence.Engine.Messages
{
    public class DisplayTextMessage : GameMessage
    {
        [NotNull]
        public string Text { get; }

        public ClientMessageType MessageType { get; }

        public DisplayTextMessage([NotNull] string text, ClientMessageType messageType)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
            MessageType = messageType;
        }

        public override string ToString() => $"Display '{Text}'";

    }
}