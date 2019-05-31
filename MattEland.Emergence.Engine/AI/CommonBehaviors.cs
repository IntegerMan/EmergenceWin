using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.AI
{
    public class CommonBehaviors
    {
        [NotNull]
        public IdleBehavior Idle { get; } = new IdleBehavior();

        [NotNull]
        public WanderBehavior Wander { get; } = new WanderBehavior();
    }
}