using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.WpfCore.ViewModels;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.ViewModels
{
    public class CommandViewModelTests
    {
        [Test]
        public void CommandShouldExposeProperties()
        {
            // Arrange
            var command = new OverloadCommand();
            var instance = new CommandSlot(command);
            var gameVM = new GameViewModel();

            // Act
            var vm = new CommandViewModel(instance, gameVM);

            // Assert
            vm.Content.ShouldBe(command.ShortName);
        }
    }
}