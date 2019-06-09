using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class PlayerSwitchTests : EmergenceTestBase
    {
        [Test]
        public void ChangingPlayerShouldChangePlayer()
        {
            // Arrange
            var direction = MoveDirection.Left;
            var pos = Player.Pos.GetNeighbor(direction, 2);
            var oldType = Player.PlayerType;
            var newType = ActorType.Player;
            var switcher = new CharacterSelectTile(pos, newType);
            AddObject(switcher);

            // Act
            GameViewModel.MovePlayer(direction);

            // Assert
            Player.PlayerType.ShouldBe(newType);
            Player.PlayerType.ShouldNotBe(oldType);
        }
    }
}