using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;

namespace MattEland.Emergence.Engine.Model.Messages
{
    public class CreatedMessage : GameMessage
    {
        public CreatedMessage([NotNull] GameObjectBase source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public GameObjectBase Source { get; }

        public override string ToString() => $"Created {Source.AsciiChar} at {Source.Pos}";
    }
}