﻿using System;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.Model.Messages
{
    public class CreatedMessage : GameMessage
    {
        public CreatedMessage([NotNull] IGameObject source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        [NotNull]
        public IGameObject Source { get; }

        public override string ToString() => $"Created {Source.AsciiChar} at {Source.Pos}";
    }
}