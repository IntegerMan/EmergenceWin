using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.GameLoop;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.WinCore.ViewModels
{
    public class GameViewModel
    {
        [NotNull]
        private readonly GameManager _gameManager;

        public GameViewModel()
        {
            _gameManager = new GameManager();
            _gameManager.Start();
            
            UpdateObjects();
        }

        private void UpdateObjects()
        {
            WorldObjects.Clear();
            
            _gameManager.Objects.Each(obj =>
            {
                var vm = new WorldObjectViewModel(obj, this);
                WorldObjects.Add(vm);
            });
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        
        public int XOffset => 40;
        public int YOffset => -35;
    }
}
