using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities.Actors;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class ActorViewModel : ViewModelBase
    {
        [NotNull]
        public Actor Actor { get; }

        public ActorViewModel([NotNull] Actor actor)
        {
            Actor = actor ?? throw new ArgumentNullException(nameof(actor));
        }

        public int Health => Actor.Stability;
        public int MaxHealth => Actor.MaxStability;

        public int Operations => Actor.Operations;
        public int MaxOperations => Actor.MaxOperations;
    }
}