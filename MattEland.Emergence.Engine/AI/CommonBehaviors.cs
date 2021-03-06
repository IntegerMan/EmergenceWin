﻿using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.AI
{
    public class CommonBehaviors
    {
        [NotNull]
        public IdleBehavior Idle { get; } = new IdleBehavior();

        [NotNull]
        public WanderBehavior Wander { get; } = new WanderBehavior();

        [NotNull]
        public MeleeAttackBehavior Melee { get; } = new MeleeAttackBehavior();
        
        [NotNull]
        public MoveTowardsEnemyBehavior MoveTowardsTarget { get; } = new MoveTowardsEnemyBehavior();

        [NotNull]
        public MoveAwayFromEnemyBehavior MoveAwayFromEnemy { get; } = new MoveAwayFromEnemyBehavior();
    }
}