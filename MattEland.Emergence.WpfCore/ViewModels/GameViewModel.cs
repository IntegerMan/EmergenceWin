using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Domain;
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
                switch (msg)
                {
                    case ObjectCreatedMessage createMessage:
                        WorldObjects.Add(new WorldObjectViewModel(createMessage.Object, this));
                        break;

                    case ObjectUpdatedMessage updateMessage:
                        // TODO Find existing VM and just update its values
                        WorldObjects.Add(new WorldObjectViewModel(updateMessage.Object, this));
                        break;
                }
            });
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        
        public int XOffset => 40;
        public int YOffset => -35;

        public void MovePlayer(MoveDirection direction) => ProcessMessages(_gameManager.MovePlayer(direction));
    }
}
