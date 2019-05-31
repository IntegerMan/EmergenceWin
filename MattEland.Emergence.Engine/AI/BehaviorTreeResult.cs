using System.Collections.Generic;
using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.AI
{
    public class BehaviorTreeResult
    {
        [NotNull, ItemNotNull]
        public ISet<ActorBehaviorBase> EvaluatedBehaviors { get; } = new HashSet<ActorBehaviorBase>();

        [CanBeNull]
        public ActorBehaviorBase SelectedBehavior { get; set; }
    }
}