using MattEland.Emergence.Engine;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Model.EngineDefinitions;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class GameManagerTests
    {
        [Test]
        public void StartingGameManagerShouldReturnMessages()
        {
            // Arrange
            var manager = new GameManager();

            // Act
            var messages = manager.Start();

            // Assert
            messages.ShouldNotBeNull();
            messages.ShouldNotBeEmpty();
        }

        [Test]
        public void StartingGameManagerShouldResultInStartedState()
        {
            // Arrange
            var manager = new GameManager();

            // Act
            manager.Start();

            // Assert
            manager.State.ShouldBe(GameStatus.Ready);
        }

        [Theory]
        [TestCase(MoveDirection.Up)]
        [TestCase(MoveDirection.Right)]
        [TestCase(MoveDirection.Down)]
        [TestCase(MoveDirection.Left)]
        public void GameManagerShouldRespondToMovesWithMessages(MoveDirection direction)
        {
            // Arrange
            var manager = new GameManager();
            manager.Start();

            // Act
            var messages = manager.MovePlayer(direction);

            // Assert
            messages.ShouldNotBeNull();
            messages.ShouldNotBeEmpty();
        }

        [Test]
        public void GameManagerShouldStartNotStarted()
        {
            // Arrange
            var manager = new GameManager();

            // Assert
            manager.State.ShouldBe(GameStatus.NotStarted);
        }

    }
}