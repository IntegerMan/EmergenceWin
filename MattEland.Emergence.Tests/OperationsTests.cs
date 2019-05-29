using MattEland.Emergence.Engine.Commands;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class OperationsTests : EmergenceTestBase
    {
        [Test]
        public void UsingCommandsShouldDetractOperations()
        {
            // Arrange
            var command = new OverloadCommand();

            // Act
            GameService.HandleCommand(command, Player.Pos);

            // Assert
            Player.Operations.ShouldBeLessThan(Player.MaxOperations);
        }

        [Test]
        public void WaitingShouldRechargeOperations()
        {
            // Arrange
            Player.Operations = 0;

            // Act
            GameService.Wait();

            // Assert
            Player.Operations.ShouldBe(1);
        }
    }
}