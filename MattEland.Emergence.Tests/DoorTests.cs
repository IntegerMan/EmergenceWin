using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Obstacles;
using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class DoorTests : EmergenceTestBase
    {

        [Test]
        public void DoorsShouldOpenWhenPlayerWalksNextToThem()
        {
            // Arrange
            var door = Context.Level.Objects.OfType<Door>().First();
            var playerStartPos = door.Pos.GetNeighbor(MoveDirection.Right).GetNeighbor(MoveDirection.Right);
            Context.TeleportActor(Player, playerStartPos);
            
            // Act
            GameViewModel.MovePlayer(MoveDirection.Left);
            
            // Assert
            door.IsOpen.ShouldBeTrue();
        }

        [Test]
        public void DoorsShouldOpenWhenPlayerEntersThem()
        {
            // Arrange
            var door = Context.Level.Objects.OfType<Door>().First();
            var playerStartPos = door.Pos.GetNeighbor(MoveDirection.Right);
            Context.TeleportActor(Player, playerStartPos);
            
            // Act
            GameViewModel.MovePlayer(MoveDirection.Left);
            
            // Assert
            door.IsOpen.ShouldBeTrue();
        }

    }
}