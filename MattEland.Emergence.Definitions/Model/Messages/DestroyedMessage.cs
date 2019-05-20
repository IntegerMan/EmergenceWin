using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Model.Entities;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class DestroyedMessage : GameMessage
    {
        public DestroyedMessage([NotNull] WorldObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public WorldObject Source { get; }

        public override string ToString() => $"Destroy object {Source.Id.ToString().Substring(0, 5)}...";
    }
}