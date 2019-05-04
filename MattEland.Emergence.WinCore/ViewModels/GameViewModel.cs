using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            WorldGenerator.generateObstacles(50, 32, 23)
                .Each(o => WorldObjects.Add(new WorldObjectViewModel(o)));
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } 
            = new ObservableCollection<WorldObjectViewModel>();
    }
}
