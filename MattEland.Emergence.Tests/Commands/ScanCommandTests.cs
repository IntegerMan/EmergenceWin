using MattEland.Emergence.Engine.Commands;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.Commands
{
    public class ScanCommandTests : EmergenceTestBase
    {
        [Test]
        public void ScanCommandShouldIncreaseVisibleAreaWhenActive()
        {
            // Arrange
            var command = SetPlayerCommand(new ScanCommand());
            var los = Player.EffectiveLineOfSightRadius;

            // Act
            GameService.HandleCommand(command, Player.Pos);

            // Assert
            command.IsActive.ShouldBeTrue();
            Player.EffectiveLineOfSightRadius.ShouldBeGreaterThan(los);
        }        
        
        [Test]
        public void ScanCommandActivateAndDeactivateShouldRestoreToOriginalSightRadius()
        {
            // Arrange
            var command = SetPlayerCommand(new ScanCommand());
            var los = Player.EffectiveLineOfSightRadius;

            // Act
            GameService.HandleCommand(command, Player.Pos);
            GameService.HandleCommand(command, Player.Pos);

            // Assert
            command.IsActive.ShouldBeFalse();
            Player.EffectiveLineOfSightRadius.ShouldBe(los);
        }
    }
}