using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.AI
{
    public abstract class ActorBehaviorBase
    {
        [NotNull] protected readonly GameCommand WaitCommand = new WaitCommand();
        [NotNull] protected readonly GameCommand MoveCommand = new MoveCommand();

        public abstract bool Evaluate([NotNull] GameContext context, [NotNull] Actor actor, [NotNull, ItemNotNull] IEnumerable<GameCell> choices);
    }
}