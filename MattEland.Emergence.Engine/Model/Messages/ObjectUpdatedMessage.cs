﻿using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Model.Messages
{
    public class ObjectUpdatedMessage : GameMessage
    {
        [NotNull]
        public GameObjectBase Source { get; }

        public ObjectUpdatedMessage([NotNull] GameObjectBase source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public override string ToString() => $"Updated {Source.AsciiChar} at {Source.Pos}";

    }
}