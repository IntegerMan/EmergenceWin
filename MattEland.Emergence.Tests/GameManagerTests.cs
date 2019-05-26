using MattEland.Emergence.Engine;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Model.EngineDefinitions;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class GameManagerTests : EmergenceTestBase
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
            // Arrange / Act
            var manager = GetStartedGameManager();

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
            var manager = GetStartedGameManager();

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