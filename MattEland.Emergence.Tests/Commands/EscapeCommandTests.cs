using MattEland.Emergence.Engine.Commands;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.Commands
{
    public class EscapeCommandTests : EmergenceTestBase
    {
        [Test]
        public void EscapeCommandShouldTeleportActor()
        {
            // Arrange
            var pos = Player.Pos;
            var command = new EscapeCommand();

            // Act
            GameService.HandleCommand(command, pos);

            // Assert
            Player.Pos.ShouldNotBe(pos);
        }
    }
}