using System;
using JetBrains.Annotations;
using MattEland.Emergence.Model.Entities;

namespace MattEland.Emergence.Model.Messages
{
    public class ObjectUpdatedMessage : GameMessage
    {
        [NotNull]
        public WorldObject Source { get; }

        public ObjectUpdatedMessage([NotNull] WorldObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }
    }
}