﻿using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class VisionTests : EmergenceTestBase
    {
        [Test]
        public void PlayerShouldBeAbleToSeeTheirOwnTile()
        {
            // Arrange

            // Act

            // Assert
            Player.EffectiveLineOfSightRadius.ShouldBeGreaterThan(0);
            Player.VisibleCells.ShouldContain(Player.Pos);
        }

        [Theory]
        [TestCase(MoveDirection.Left)]
        [TestCase(MoveDirection.Up)]
        [TestCase(MoveDirection.Down)]
        [TestCase(MoveDirection.Right)]
        public void PlayerShouldBeAbleToSeeNeighboringTiles(MoveDirection direction)
        {
            // Arrange
            var target = Player.Pos.GetNeighbor(direction);

            // Act

            // Assert
            Player.VisibleCells.ShouldContain(target);
        }
    }
}