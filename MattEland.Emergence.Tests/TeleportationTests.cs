using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class TeleportationTests : EmergenceTestBase
    {

        [SetUp]
        public void Initialize()
        {
            InitializeGameService();

            Context.ClearMessages();
        }

        [Test]
        public void TeleportActorShouldMoveThem()
        {
            // Arrange
            var oldPos = Player.Pos;
            var targetPos = oldPos.GetNeighbor(MoveDirection.Left);

            // Act
            Context.TeleportActor(Player, targetPos);

            // Assert
            Player.Pos.ShouldBe(targetPos);
            Context.Level.GetCell(targetPos).Actor.ShouldBe(Player);
            Context.Level.GetCell(oldPos).Actor.ShouldNotBe(Player);
        }
    }
}