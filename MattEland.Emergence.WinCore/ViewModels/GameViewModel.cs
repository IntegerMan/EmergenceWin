using System.Collections.Generic;
using System.Collections.ObjectModel;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class GameViewModel
    {
        public GameViewModel()
        {
            WorldGenerator.generateMap(0)
                          .Each(o => WorldObjects.Add(new WorldObjectViewModel(o, this)));

            int i = 42;
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        
        public int XOffset => 40;
        public int YOffset => -35;
    }
}
