using MattEland.Emergence.Engine.Model;
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
            var manager = BuildGameService();

            // Act
            var context = manager.StartNewGame();

            // Assert
            context.ShouldNotBeNull();
            context.Messages.ShouldNotBeEmpty();
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
            var context = manager.MovePlayer(direction);

            // Assert
            context.ShouldNotBeNull();
            context.Messages.ShouldNotBeEmpty();
        }


        [Test]
        public void GameManagerShouldStartNotStarted()
        {
            // Arrange
            var service = BuildGameService();

            // Assert
            service.State.ShouldBe(GameStatus.NotStarted);
        }

    }
}