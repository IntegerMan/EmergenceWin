using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.WpfCore.ViewModels;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.Commands
{
    public class SwapCommandTests : EmergenceTestBase
    {

        [Theory]
        [TestCase(-15, 46)]
        [TestCase(-12, 43)]
        public void SwapWithinRangeShouldTeleportThePlayer(int x, int y)
        {
            // Arrange
            var slot = SetPlayerCommand(new SwapCommand());
            Pos2D targetPos = new Pos2D(x, y);

            // Act
            GameViewModel.HandleCommand(slot);
            GameViewModel.HandleTargetedCommandInput(targetPos);

            // Assert
            GameViewModel.UIState.ShouldBe(UIState.ReadyForInput);
            GameViewModel.Player.Actor.Pos.ShouldBe(targetPos);
        }        
        
        [Theory]
        [TestCase(-25, 44)]
        [TestCase(-20, 39)]
        [TestCase(2000, -2000)]
        public void SwapOutsideVisibleRangeNotShouldTeleportThePlayer(int x, int y)
        {
            // Arrange
            var slot = SetPlayerCommand(new SwapCommand());
            var targetPos = new Pos2D(x, y);
            var originalPos = Player.Pos;

            // Act
            GameViewModel.HandleCommand(slot);
            GameViewModel.HandleTargetedCommandInput(targetPos);

            // Assert
            GameViewModel.UIState.ShouldBe(UIState.ReadyForInput);
            GameViewModel.Player.Actor.Pos.ShouldBe(originalPos);
        }
    }
}