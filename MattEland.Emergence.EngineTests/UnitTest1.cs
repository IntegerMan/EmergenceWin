using MattEland.Emergence.Engine;
using MattEland.Emergence.Model;
using Shouldly;
using Xunit;

namespace MattEland.Emergence.EngineTests
{
    public class GameManagerTests
    {
        [Fact]
        public void MovingPlayerShouldResultInANewPlayerPosition()
        {
            // Arrange
            var gameManager = new GameManager();
            gameManager.Start();
            var initialPos = gameManager.Player.Pos;

            // Act
            gameManager.MovePlayer(MoveDirection.Left);

            // Assert
            gameManager.Player.Pos.ShouldBe(initialPos.GetNeighbor(MoveDirection.Left));
        }
    }
}