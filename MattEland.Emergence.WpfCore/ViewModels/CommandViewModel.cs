using System;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;

namespace MattEland.Emergence.WpfCore.ViewModels
{
    public class CommandViewModel : ViewModelBase
    {
        [NotNull] private readonly GameViewModel _game;

        [CanBeNull]
        public CommandSlot CommandSlot { get; }

        public CommandViewModel([CanBeNull] CommandSlot commandSlot, [NotNull] GameViewModel game)
        {
            _game = game ?? throw new ArgumentNullException(nameof(game));
            CommandSlot = commandSlot;
        }

        public string Content => CommandSlot?.Command != null ? CommandSlot.Command.ShortName : string.Empty;

        public void Execute()
        {
            _game.HandleCommand(CommandSlot);
        }
    }
}