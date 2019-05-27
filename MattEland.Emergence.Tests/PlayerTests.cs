using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class PlayerTests : EmergenceTestBase
    {
        [Test]
        public void PlayerShouldUseAGreenAtSign()
        {
            // Arrange

            // Act

            // Assert
            Player.ForegroundColor.ShouldBe(GameColors.Green);
            Player.AsciiChar.ShouldBe('@');
        }

    }
}