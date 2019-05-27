using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MattEland.Emergence.Engine;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Messages;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class GameViewModel
    {
        private readonly IDictionary<Guid, WorldObjectViewModel> _objects = new Dictionary<Guid, WorldObjectViewModel>();

        private readonly GameService _gameManager;

        public GameViewModel()
        {
            _gameManager = new GameService();

            GameCreationConfigurator.ConfigureObjectCreation();

            var context = _gameManager.StartNewGame();

            ProcessMessages(context.Messages);
        }

        private void ProcessMessages(IEnumerable<GameMessage> messages)
        {
            Messages.Clear();

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

                    case MovedMessage moveMessage:
                        vm = _objects[moveMessage.Source.Id];
                        vm.UpdatePosition(moveMessage.NewPos);
                        break;
                    
                    case ObjectUpdatedMessage updateMessage:
                        vm = _objects[updateMessage.Source.Id];
                        vm.UpdateFrom(updateMessage);
                        break;

                    case DestroyedMessage destroyedMessage:
                        _objects.Remove(destroyedMessage.Source.Id);
                        WorldObjects.Where(o => o.Id == destroyedMessage.Source.Id).EachSafe(o => WorldObjects.Remove(o));
                        break;
                }

                Messages.Add(new MessageViewModel(msg));
            });
            
            CenterOnPlayer();
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        public IList<MessageViewModel> Messages { get; } = new ObservableCollection<MessageViewModel>();
        
        public int XOffset { get; set; } = 40;

        public int YOffset { get; set; } = -35;

        public void MovePlayer(MoveDirection direction)
        {
            var context = _gameManager.MovePlayer(direction);
            ProcessMessages(context.Messages);
        }

        private void CenterOnPlayer()
        {
            if (!WorldObjects.Any()) return;

            var player = WorldObjects.Select(o => o.Source).OfType<Player>().FirstOrDefault();
            if (player != null)
            {
                CenterOn(player.Pos);
            }
        }

        public void CenterOn(Pos2D pos)
        {
            XOffset = -(pos.X - 15);
            YOffset = -(pos.Y - 10);

            WorldObjects.Each(o => { o.NotifyOffsetChanged(); });
        }
    }
}
