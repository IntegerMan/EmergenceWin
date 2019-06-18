using JetBrains.Annotations;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Actions
{
    public abstract class GameActionBase
    {
        public abstract void Execute([NotNull] GameContext context);
    }
}