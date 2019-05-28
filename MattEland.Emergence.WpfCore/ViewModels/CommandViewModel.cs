using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class CommandViewModel : ViewModelBase
    {
        [NotNull] private readonly GameViewModel _game;

        [CanBeNull]
        public CommandInstance CommandInstance { get; }

        public CommandViewModel([CanBeNull] CommandInstance commandInstance, [NotNull] GameViewModel game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            CommandInstance = commandInstance;
        }

        public string Content => CommandInstance?.Command != null ? CommandInstance.Command.ShortName : string.Empty;

        public void Execute()
        {
            _game.HandleCommand(CommandInstance);
        }
    }
}