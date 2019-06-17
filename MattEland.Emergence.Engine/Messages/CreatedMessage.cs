using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Messages
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
        
        public override string ForegroundColor => GameColors.LightGreen;
    }
}