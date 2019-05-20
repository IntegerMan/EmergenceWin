using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class ObjectUpdatedMessage : GameMessage
    {
        [NotNull]
        public IGameObject Source { get; }

        public ObjectUpdatedMessage([NotNull] IGameObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override string ToString() => $"Updated {Source.AsciiChar} at {Source.Pos}";

    }
}