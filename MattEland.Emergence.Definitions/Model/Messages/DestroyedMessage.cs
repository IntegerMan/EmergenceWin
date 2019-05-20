using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class DestroyedMessage : GameMessage
    {
        public DestroyedMessage([NotNull] IGameObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public IGameObject Source { get; }

        public override string ToString() => $"Destroy object {Source.ObjectId.ToString().Substring(0, 5)}...";
    }
}