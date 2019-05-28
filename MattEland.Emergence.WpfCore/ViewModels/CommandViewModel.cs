using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class CommandViewModel : INotifyPropertyChanged
    {
        private readonly GameViewModel _game;
        public CommandInstance CommandInstance { get; }

        public CommandViewModel([NotNull] CommandInstance commandInstance, GameViewModel game)
        {
            _game = game;
            CommandInstance = commandInstance ?? throw new ArgumentNullException(nameof(commandInstance));
        }

        public string Content => CommandInstance.Command != null ? CommandInstance.Command.ShortName : string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Execute()
        {
            if (CommandInstance.Command != null)
            {
                _game.HandleCommand(CommandInstance.Command);
            }
        }
    }
}