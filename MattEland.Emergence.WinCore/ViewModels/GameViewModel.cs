using System.Collections.Generic;
using System.Collections.ObjectModel;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            var map = WorldGenerator.generateMap(31, 22);
            map.Each(o => WorldObjects.Add(new WorldObjectViewModel(o)));
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
    }
}
