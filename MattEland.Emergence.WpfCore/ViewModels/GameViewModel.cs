using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using JetBrains.Annotations;
using MattEland.Emergence.Engine;
using MattEland.Emergence.Model;
using MattEland.Emergence.Model.Messages;
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
                    case CreatedMessage createMessage:
                        vm = new WorldObjectViewModel(createMessage.Source, this);
                        WorldObjects.Add(vm);
                        _objects[vm.Id] = vm;
                        break;

                    case ObjectUpdatedMessage updateMessage:
                        vm = _objects[updateMessage.Source.Id];
                        vm.UpdateFrom(updateMessage);
                        break;

                    case DisplayTextMessage displayMessage:
                        MessageBox.Show(displayMessage.Text, "Display Message", MessageBoxButton.OK, MessageBoxImage.Information);
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
