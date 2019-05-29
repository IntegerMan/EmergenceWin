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
            var command = new CommandInstance(new OverloadCommand());

            // Act
            GameViewModel.HandleCommand(command);

            // Assert
            Player.Operations.ShouldBeLessThan(Player.MaxOperations);
        }

        [Test]
        public void WaitingShouldRechargeOperations()
        {
            // Arrange
            Player.Operations = 0;

            // Act
            GameViewModel.Wait();

            // Assert
            Player.Operations.ShouldBe(1);
        }
    }
}