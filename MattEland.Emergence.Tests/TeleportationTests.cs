using MattEland.Emergence.Engine.Entities;
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

        [Test]
        public void TeleportActorIntoObstacleShouldFail()
        {
            // Arrange
            var oldPos = Player.Pos;
            var targetPos = oldPos.GetNeighbor(MoveDirection.Up);
            var oldHealth = Player.Stability;

            // Act
            Context.TeleportActor(Player, targetPos);

            // Assert
            Player.Pos.ShouldBe(oldPos);
            Context.Level.GetCell(targetPos).Actor.ShouldNotBe(Player);
            Context.Level.GetCell(oldPos).Actor.ShouldBe(Player);
            Player.Stability.ShouldBeLessThan(oldHealth);
        }

        [Test]
        public void TeleportActorIntoActorShouldSwapAndCauseDamage()
        {
            // Arrange
            var oldPos = Player.Pos;
            var targetPos = oldPos.GetNeighbor(MoveDirection.Right);
            var turret = CreateTurret(targetPos);

            // Act
            Context.TeleportActor(Player, targetPos);

            // Assert
            Player.Pos.ShouldBe(targetPos);
            turret.Pos.ShouldBe(oldPos);
            Context.Level.GetCell(oldPos).Actor.ShouldBe(turret);
            Context.Level.GetCell(targetPos).Actor.ShouldBe(Player);
            Player.Stability.ShouldBeLessThan(Player.MaxStability);
            turret.Stability.ShouldBeLessThan(turret.MaxStability);
        }
    }
}