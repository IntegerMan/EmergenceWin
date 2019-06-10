using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
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
        
        [NotNull]
        private readonly ISet<Pos2D> _knownCells = new HashSet<Pos2D>();

        private Visibility _gameOverVisibility = Visibility.Collapsed;
        private Visibility _gameVisibility = Visibility.Visible;

        public GameViewModel()
        {
            _gameService = new GameService();

            Update(_gameService.StartNewGame());
        }

        public UIState UIState
        {
            get => _uiState;
            private set
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

        public void ProcessMessage([NotNull] GameMessage message)
        {
            WorldObjectViewModel vm;
            switch (message)
            {
                case CreatedMessage createMessage:
                    vm = new WorldObjectViewModel(createMessage.Source, this);
                    _objects[vm.Id] = vm;
                    break;

                case MovedMessage moveMessage:
                    vm = GetObject(moveMessage.Source.Id);
                    vm.UpdatePosition(moveMessage.NewPos);
                    break;
                    
                case ObjectUpdatedMessage updateMessage:
                    vm = GetObject(updateMessage.Source.Id);
                    vm.UpdateFrom(updateMessage);
                    break;
                    
                case ChangedLevelMessage _:
                    _knownCells.Clear();
                    break;

                case DestroyedMessage destroyedMessage:
                    _objects.Remove(destroyedMessage.Source.Id);
                    VisibleWorldObjects.Where(o => o.Id == destroyedMessage.Source.Id).EachSafe(o => VisibleWorldObjects.Remove(o));
                    break;
                
                case VisibleCellsMessage visibleMessage:
                    UpdateVisibleCells(visibleMessage);
                    break;
            }

            Messages.Add(new MessageViewModel(message));
        }

        private void UpdateVisibleCells(VisibleCellsMessage visible)
        {
            visible.Cells.Each(p => _knownCells.Add(p));
            
            _objects.Values.Each(o =>
            {
                bool wasVisible = o.IsVisible;
                o.IsVisible = visible.Cells.Contains(o.Source.Pos);

                if (wasVisible != o.IsVisible && o.IsVisible)
                {
                    VisibleWorldObjects.Add(o);
                }
                
                o.OnIsKnownInvalidated();
            });
        }

        private void ProcessMessages([NotNull, ItemNotNull] IEnumerable<GameMessage> messages)
        {
            Messages.Clear();

            messages.Each(ProcessMessage);

            CenterOnPlayer();
        }

        private WorldObjectViewModel GetObject(Guid id)
        {
            if (_objects.ContainsKey(id))
            {
                return _objects[id];
            }
            
            throw new InvalidOperationException($"Could not find a view model for an object with an ID of {id}");
        }

        private void UpdateCommands(GameContext context)
        {
            Commands.Clear();

            context.Player.HotbarCommands.Each(c => Commands.Add(new CommandViewModel(c, this)));
        }

        public IList<WorldObjectViewModel> VisibleWorldObjects { get; } = new ObservableCollection<WorldObjectViewModel>();
        
        public IList<CommandViewModel> Commands { get; } = new ObservableCollection<CommandViewModel>();
        public IList<MessageViewModel> Messages { get; } = new ObservableCollection<MessageViewModel>();
        
        public int XOffset { get; private set; } = 40;

        public int YOffset { get; private set; } = -35;

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
            UpdateGameState(context);
        }

        private void UpdateGameState(GameContext context)
        {
            if (context.IsGameOver)
            {
                GameOverVisibility = Visibility.Visible;
                GameVisibility = Visibility.Collapsed;

                UIState = UIState.GameOver;
            }
            else
            {
                GameOverVisibility = Visibility.Collapsed;
                GameVisibility = Visibility.Visible;
            
                UIState = UIState.ReadyForInput;
            }
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

        public Visibility GameOverVisibility
        {
            get => _gameOverVisibility;
            set
            {
                if (value == _gameOverVisibility) return;
                _gameOverVisibility = value;
                OnPropertyChanged();
            }
        }

        public Visibility GameVisibility
        {
            get => _gameVisibility;
            set
            {
                if (value == _gameVisibility) return;
                _gameVisibility = value;
                OnPropertyChanged();
            }
        }

        private void CenterOnPlayer()
        {
            if (!VisibleWorldObjects.Any()) return;

            var player = VisibleWorldObjects.Select(o => o.Source).OfType<Player>().FirstOrDefault();
            if (player != null)
            {
                CenterOn(player.Pos);
            }
        }

        public void CenterOn(Pos2D pos)
        {
            XOffset = -(pos.X - 25);
            YOffset = -(pos.Y - 15);

            VisibleWorldObjects.Each(o => { o.NotifyOffsetChanged(); });
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

        public bool IsCellKnownToPlayer(Pos2D pos) => _knownCells.Contains(pos);
    }
}
