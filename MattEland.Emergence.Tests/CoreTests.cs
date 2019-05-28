using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CoreTests : EmergenceTestBase
    {

        [Test]
        public void MovingIntoACoreShouldCaptureIt()
        {
            // Arrange
            var direction = MoveDirection.Left;
            var targetPos = Player.Pos.GetNeighbor(direction);
            var core = GameObjectFactory.CreateCore(targetPos);
            Context.AddObject(core);
            Context.ClearMessages();

            // Act
            GameService.MovePlayer(direction);

            // Assert
            core.ActualTeam.ShouldBe(Alignment.Player);
        }
    }
}