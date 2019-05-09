using MattEland.Emergence.Domain;
using MattEland.Emergence.GameLoop;
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
            var initialPos = gameManager.Player.Value.Position;

            // Act
            gameManager.MovePlayer(MoveDirection.Left);

            // Assert
            gameManager.Player.Value.Position.ShouldBe(initialPos.GetNeighbor(MoveDirection.Left));
        }
    }
}