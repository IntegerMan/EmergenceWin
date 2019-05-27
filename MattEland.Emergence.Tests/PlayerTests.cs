using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class PlayerTests : EmergenceTestBase
    {

        [SetUp]
        public void Initialize()
        {
            base.InitializeGameService();

            Context.ClearMessages();
        }


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