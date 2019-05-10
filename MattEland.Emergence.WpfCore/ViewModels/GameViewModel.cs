using System;
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
        private readonly IDictionary<Guid, WorldObjectViewModel> _objects = new Dictionary<Guid, WorldObjectViewModel>();

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
                WorldObjectViewModel vm;
                switch (msg)
                {
                    case ObjectCreatedMessage createMessage:
                        vm = new WorldObjectViewModel(createMessage.Object, this);
                        WorldObjects.Add(vm);
                        _objects[vm.Id] = vm;
                        break;

                    case ObjectUpdatedMessage updateMessage:
                        vm = _objects[updateMessage.Object.Id];
                        vm.UpdateFrom(updateMessage);
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
