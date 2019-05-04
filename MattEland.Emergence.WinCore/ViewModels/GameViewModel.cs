using System;
using System.Collections.ObjectModel;
using MattEland.Emergence.Domain;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            WorldObjects.Add(new WorldObjectViewModel(new Obstacle(new Position(2, 2), 10 )));
            WorldObjects.Add(new WorldObjectViewModel(new Obstacle(new Position(4, 1), 10 )));
        }

        public ObservableCollection<WorldObjectViewModel> WorldObjects { get; } 
            = new ObservableCollection<WorldObjectViewModel>();
    }
}
