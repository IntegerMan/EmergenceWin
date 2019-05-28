using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class CommandViewModel : INotifyPropertyChanged
    {
        public ICommandInstance CommandInstance { get; }

        public CommandViewModel([NotNull] ICommandInstance commandInstance)
        {
            CommandInstance = commandInstance ?? throw new ArgumentNullException(nameof(commandInstance));
        }

        public string Content => CommandInstance.Command != null ? CommandInstance.Command.ShortName : string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}