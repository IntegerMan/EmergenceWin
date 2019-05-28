using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.WpfCore.ViewModels;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.ViewModels
{
    public class MessageViewModelTests
    {
        [Test]
        public void MessageViewModelShouldExposeProperties()
        {
            // Arrange
            var text = "Bit the big one";
            var message = new TauntEffect(GameObjectFactory.CreatePlayer(Actors.PlayerDebugger), text);

            // Act
            var vm = new MessageViewModel(message);

            // Assert
            vm.Message.ShouldBe(message);
            vm.Text.ShouldBe($"Taunt: {text}");
        }
    }
}