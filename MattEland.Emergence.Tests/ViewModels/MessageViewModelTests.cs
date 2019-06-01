using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
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
            var source = GameObjectFactory.CreatePlayer(new Pos2D(0,0), ActorType.Player);
            var message = new TauntEffect(source, text);

            // Act
            var vm = new MessageViewModel(message);

            // Assert
            vm.Message.ShouldBe(message);
            vm.Text.ShouldBe($"Taunt: {text}");
        }
    }
}