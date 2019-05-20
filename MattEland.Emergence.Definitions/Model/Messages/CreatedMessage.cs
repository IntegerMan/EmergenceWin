using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Model.Entities;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class CreatedMessage : GameMessage
    {
        public CreatedMessage([NotNull] WorldObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public WorldObject Source { get; }

        public override string ToString() => $"Created {Source.AsciiChar} at {Source.Pos}";
    }
}