using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Actions
{
    public class DeleteObjectAction : GameActionBase
    {
        [NotNull]
        private GameObjectBase _target;

        public DeleteObjectAction([NotNull] GameObjectBase target)
        {
            _target = target ?? throw new ArgumentNullException(nameof(target));
        }

        public override void Execute(GameContext context)
        {
            context.RemoveObject(_target);
        }
    }
}