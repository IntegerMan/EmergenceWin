using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class WallTests : EmergenceTestBase
    {

        [Test]
        public void DestroyingAWallShouldCreateRubble()
        {
            // Arrange
            var pos = Player.Pos.Add(0, -3);
            var cell = Context.Level.GetCell(pos);
            var wall = cell.Objects.OfType<Wall>().Single();

            // Act
            Context.CombatManager.HurtObject(Context, Player, wall, wall.MaxStability, "Hacks", DamageType.Normal);

            // Assert
            cell.Objects.OfType<Debris>().Count().ShouldBe(1);
        }

        [Test]
        public void DestroyingAWallShouldRemoveIt()
        {
            // Arrange
            var pos = Player.Pos.Add(0, -3);
            var cell = Context.Level.GetCell(pos);
            var wall = cell.Objects.OfType<Wall>().Single();

            // Act
            Context.CombatManager.HurtObject(Context, Player, wall, wall.MaxStability, "Hacks", DamageType.Normal);

            // Assert
            wall.IsDead.ShouldBeTrue();
            cell.Objects.ShouldNotContain(wall);
        }

        [Test]
        public void DestroyingAWallShouldCreateAdditionalWallsIfNeeded()
        {
            // Arrange
            var pos = Player.Pos.Add(0, -3);
            var behindPos = pos.GetNeighbor(MoveDirection.Up);

            var cell = Context.Level.GetCell(pos);
            var wall = cell.Objects.OfType<Wall>().Single();

            var behindCell = Context.Level.GetCell(behindPos);
            behindCell.ShouldBeNull();

            // Act
            Context.CombatManager.HurtObject(Context, Player, wall, wall.MaxStability, "Hacks", DamageType.Normal);

            // Assert
            behindCell = Context.Level.GetCell(behindPos);
            behindCell.ShouldNotBeNull();
            behindCell.Objects.OfType<Wall>().Count().ShouldBe(1);
        }
    }
}
