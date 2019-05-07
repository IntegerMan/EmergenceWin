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

            var messages = _gameManager.Start();
            ProcessMessages(messages);
        }

        private void ProcessMessages(IEnumerable<GameMessage> messages)
        {
            messages.Each(msg =>
            {
                if (msg is ObjectCreatedMessage createMessage)
                {
                    WorldObjects.Add(new WorldObjectViewModel(createMessage.Object, this));
                }
            });
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        
        public int XOffset => 40;
        public int YOffset => -35;
    }
}
