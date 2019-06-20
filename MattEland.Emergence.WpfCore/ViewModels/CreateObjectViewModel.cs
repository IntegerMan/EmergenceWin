using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class CreateObjectViewModel : ViewModelBase
    {
        [NotNull]
        private string _selectedActorType = ActorType.Bug.ToString("G");

        [NotNull] 
        public WorldObjectViewModel WorldObject { get; }
        
        [NotNull] 
        public GameViewModel Game { get; }

        public CreateObjectViewModel([NotNull] WorldObjectViewModel objectViewModel, [NotNull] GameViewModel gameViewModel)
        {
            WorldObject = objectViewModel ?? throw new ArgumentNullException(nameof(objectViewModel));
            Game = gameViewModel ?? throw new ArgumentNullException(nameof(gameViewModel));
        }

        [NotNull, ItemNotNull]
        public IEnumerable<string> ActorTypes => Enum.GetNames(typeof(ActorType));

        [NotNull]
        public string SelectedActorType
        {
            get => _selectedActorType;
            set
            {
                if (value == _selectedActorType) return;
                _selectedActorType = value;
                OnPropertyChanged();
            }
        }
    }
}