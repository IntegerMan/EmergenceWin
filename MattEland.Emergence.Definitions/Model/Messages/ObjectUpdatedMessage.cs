using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Model.Entities;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class ObjectUpdatedMessage : GameMessage
    {
        [NotNull]
        public WorldObject Source { get; }

        public ObjectUpdatedMessage([NotNull] WorldObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override string ToString() => $"Updated {Source.AsciiChar} at {Source.Pos}";

    }
}