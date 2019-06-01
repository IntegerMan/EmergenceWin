using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Messages;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class GameViewModel : ViewModelBase
    {
        private readonly IDictionary<Guid, WorldObjectViewModel> _objects = new Dictionary<Guid, WorldObjectViewModel>();

        private readonly GameService _gameService;
        private ActorViewModel _player;
        private UIState _uiState;
        private CommandSlot _targetedCommand;

        public GameViewModel()
        {
            _gameService = new GameService();

            Update(_gameService.StartNewGame());
        }

        public UIState UIState
        {
            get => _uiState;
            set
            {
                if (value == _uiState) return;
                _uiState = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UIPrompt));
            }
        }

        public CommandSlot TargetedCommand
        {
            get => _targetedCommand;
            set
            {
                if (Equals(value, _targetedCommand)) return;
                _targetedCommand = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(UIPrompt));
            }
        }

        public string UIPrompt
        {
            get
            {
                switch (UIState)
                {
                    case UIState.ReadyForInput: return "Ready";
                    case UIState.Executing: return "Thinking...";
                    case UIState.SelectingTarget: return $"Select a Target for {TargetedCommand?.Command?.Name}";
                    case UIState.GameOver: return "Game Over";
                    default: return $"Unknown UIState: {UIState:G}";
                }
            }
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

        private void UpdateCommands(GameContext context)
        {
            Commands.Clear();

            context.Player.HotbarCommands.Each(c => Commands.Add(new CommandViewModel(c, this)));
        }

        public IList<WorldObjectViewModel> WorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        public IList<CommandViewModel> Commands { get; } = new ObservableCollection<CommandViewModel>();
        public IList<MessageViewModel> Messages { get; } = new ObservableCollection<MessageViewModel>();
        
        public int XOffset { get; set; } = 40;

        public int YOffset { get; set; } = -35;

        public void MovePlayer(MoveDirection direction)
        {
            UIState = UIState.Executing;
            Update(_gameService.MovePlayer(direction));
        }

        private void Update(GameContext context)
        {
            Context = context;

            Player = new ActorViewModel(context.Player);

            ProcessMessages(context.Messages);
            UpdateCommands(context);
            UIState = UIState.ReadyForInput;
        }

        public GameContext Context { get; set; }

        public ActorViewModel Player
        {
            get => _player;
            set
            {
                if (Equals(value, _player)) return;
                _player = value;
                OnPropertyChanged();
            }
        }

        public GameService GameService => _gameService;

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
            XOffset = -(pos.X - 25);
            YOffset = -(pos.Y - 15);

            WorldObjects.Each(o => { o.NotifyOffsetChanged(); });
        }

        public void HandleCommand(CommandSlot slot)
        {
            var command = slot.Command;

            if (command == null) throw new InvalidOperationException("Cannot execute an empty command");

            TargetedCommand = slot;

            switch (command.ActivationType)
            {
                case CommandActivationType.Simple:
                case CommandActivationType.Active:
                    UIState = UIState.Executing;
                    Update(_gameService.HandleCommand(command, _gameService.Player.Pos));
                    break;

                case CommandActivationType.Targeted:
                    UIState = UIState.SelectingTarget;
                    break;

                default:
                    throw new NotSupportedException($"{command.ActivationType:G} commands are not supported from the UI");
            }
        }

        public void Wait() => Update(GameService.Wait());

        public void HandleTargetedCommandInput(Pos2D pos)
        {
            if (UIState != UIState.SelectingTarget) return;

            UIState = UIState.Executing;
            Update(_gameService.HandleCommand(TargetedCommand, pos));
        }
    }
}
