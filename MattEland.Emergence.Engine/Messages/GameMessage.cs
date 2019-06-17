using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.Messages
{
    public abstract class GameMessage
    {
        /// <summary>
        /// Gets the foreground color for the message to display in the UI. This should be a hex color.
        /// </summary>
        [NotNull]
        public abstract string ForegroundColor { get; }
    }
}