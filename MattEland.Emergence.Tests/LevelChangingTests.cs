using System.Linq;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Messages;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class LevelChangingTests : EmergenceTestBase
    {
        [Test]
        public void ChangingLevelsShouldYieldDestroyedMessages()
        {
            // Arrange
            int numObjects = Context.Level.Objects.Count(o => !o.IsPlayer);

            // Act
            Context.AdvanceToNextLevel();

            // Assert
            Context.Messages.ShouldNotBeEmpty();
            Context.Messages.OfType<DestroyedMessage>().Count().ShouldBe(numObjects);
        }

        [Test]
        public void ChangingLevelsShouldYieldCreatedMessages()
        {
            // Arrange

            // Act
            Context.AdvanceToNextLevel();

            // Assert
            Context.Messages.ShouldNotBeEmpty();
            Context.Messages.OfType<CreatedMessage>().Count().ShouldBeGreaterThan(0);
        }

        [Test]
        public void ChangingLevelsShouldKeepTheSamePlayerInstance()
        {
            // Arrange
            var player = Player;

            // Act
            Context.AdvanceToNextLevel();

            // Assert
            Player.ShouldBe(player);
        }

        [Test]
        public void ChangingLevelRepeatedlyShouldEndGame()
        {
            // Arrange
            Context.SwitchToLevel(LevelType.RouterGateway);

            // Act
            Context.AdvanceToNextLevel();

            // Assert
            Context.IsGameOver.ShouldBeTrue();
        }

    }
}