using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Model.Messages
{
    public class DestroyedMessage : GameMessage
    {
        public DestroyedMessage([NotNull] GameObjectBase source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public GameObjectBase Source { get; }

        public override string ToString() => $"Destroy object {Source.ObjectId.ToString().Substring(0, 5)}...";
    }
}