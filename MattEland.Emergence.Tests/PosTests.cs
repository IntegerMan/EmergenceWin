using MattEland.Emergence.Engine.Level;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class PosTests
    {
        [Theory]
        [TestCase(0, 0, 1, 1, 2)]
        [TestCase(0, 0, 0, 0, 0)]
        [TestCase(-13, 45, -12, 45, 1)]
        [TestCase(-13, 45, -12, 44, 2)]
        public void DistanceFromInMovesTests(int x1, int y1, int x2, int y2, int expected)
        {
            // Arrange
            var pos1 = new Pos2D(x1, y1);
            var pos2 = new Pos2D(x2, y2);

            // Act
            var distance = pos1.CalculateDistanceInMovesFrom(pos2);

            // Assert
            distance.ShouldBe(expected);
        }
    }
}