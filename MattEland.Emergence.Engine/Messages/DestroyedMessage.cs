using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Messages
{
    public class DestroyedMessage : GameMessage
    {
        public DestroyedMessage([NotNull] GameObjectBase source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public GameObjectBase Source { get; }

        public override string ToString() => $"Destroy {Source.Name} at {Source.Pos}";

        public override string ForegroundColor => GameColors.Red;
    }
}