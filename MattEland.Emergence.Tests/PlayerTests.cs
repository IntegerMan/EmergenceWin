using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class PlayerTests : EmergenceTestBase
    {
        [Test]
        public void PlayerShouldUseAGreenAtSign()
        {
            // Arrange

            // Act

            // Assert
            Player.ForegroundColor.ShouldBe(GameColors.Green);
            Player.AsciiChar.ShouldBe('@');
        }


        [TestCase(PlayerType.Forecast)]
        public void ChangePlayerShouldChangePlayer(PlayerType newType)
        {
            // Arrange
            var oldPlayer = Player;
            var newPlayer = GameObjectFactory.CreatePlayer(new Pos2D(0,0), newType);

            // Act
            Context.ReplacePlayer(newPlayer);

            // Assert
            Player.ShouldBe(newPlayer);
            Context.Messages.ShouldNotBeEmpty();
            Player.Id.ShouldBe(oldPlayer.Id);
        }
    }
}