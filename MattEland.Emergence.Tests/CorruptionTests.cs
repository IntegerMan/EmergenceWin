using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Collections;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CorruptionTests : EmergenceTestBase
    {
        [SetUp]
        public void Initialize()
        {
            base.InitializeGameService();

            Context.ClearMessages();
        }

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
    }
}