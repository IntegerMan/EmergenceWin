using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class PlayerSwitchTests : EmergenceTestBase
    {
        [TestCase(PlayerType.Game)]
        public void ChangingPlayerShouldChangePlayer(PlayerType newType)
        {
            // Arrange
            var direction = MoveDirection.Left;
            var pos = Player.Pos.GetNeighbor(direction);
            var oldType = Player.PlayerType;
            var switcher = new CharacterSelectTile(pos, newType);
            AddObject(switcher);

            // Act
            GameViewModel.MovePlayer(direction);

            // Assert
            oldType.ShouldNotBe(newType);
            Player.PlayerType.ShouldBe(newType);
            Player.PlayerType.ShouldNotBe(oldType);
        }
    }
}