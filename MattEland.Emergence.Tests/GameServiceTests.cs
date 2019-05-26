using System;
using MattEland.Emergence.Engine.Game;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class GameServiceTests : EmergenceTestBase
    {
        [Test]
        public void StartingTheGameShouldReturnMessages()
        {
            // Arrange
            var service = new GameService();

            // Act
            var context = service.StartNewGame();

            // Assert
            context.ShouldNotBeNull();
            context.Messages.ShouldNotBeEmpty();
        }
    }
}
