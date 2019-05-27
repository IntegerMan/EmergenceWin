using System.Linq;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Collections;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CorruptionTests : EmergenceTestBase
    {
        [Test]
        public void CorruptionDamageShouldCauseCorruption()
        {
            // Arrange
            var turret = CreateTurret(Player.Pos.GetNeighbor(MoveDirection.Left));

            // Act
            Context.CombatManager.HurtObject(Context, Player, turret, 1, "Corruption", DamageType.Corruption);

            // Assert
            turret.Corruption.ShouldBeGreaterThan(0);
        }
        
        [Test]
        public void CorruptNearbyShouldCorruptNearbyTiles()
        {
            // Arrange

            // Act
            Player.Pos.CorruptNearby(Context, Player);

            // Assert
            Context.Level.GetCellsInSquare(Player.Pos, 1).Each(c => c.Corruption.ShouldBeGreaterThan(0));
        }
        
        [Test]
        public void CleanseNearbyShouldCleanseNearbyTiles()
        {
            // Arrange
            var cell = Context.Level.GetCell(Player.Pos);
            cell.Corruption = 1;

            // Act
            CorruptionHelper.CleanseNearby(Context, Player, Player.Pos);

            // Assert
            cell.Corruption.ShouldBe(0);
        }

        [Test]
        public void SpreadCorruptionShouldCauseCorruptionToSpread()
        {
            // Arrange
            int numCells = Context.Level.Cells.Count;
            var cell = Context.Level.GetCell(Player.Pos);
            cell.Corruption = 1;
            int initialCorruption = Context.Level.Cells.Sum(c => c.Corruption);

            // Act
            CorruptionHelper.SpreadCorruption(Context, numCells ^ 2);

            // Assert
            Context.Level.Cells.Sum(c => c.Corruption).ShouldBeGreaterThan(initialCorruption);
        }
    }
}