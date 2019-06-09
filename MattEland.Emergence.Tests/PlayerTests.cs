﻿using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
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


        [Test]
        public void ChangePlayerShouldChangePlayer()
        {
            // Arrange
            var oldPlayer = Player;
            var newPlayer = GameObjectFactory.CreatePlayer(new Pos2D(0,0), ActorType.Player);

            // Act
            Context.ReplacePlayer(newPlayer);

            // Assert
            Player.ShouldBe(newPlayer);
            Context.Messages.ShouldNotBeEmpty();
            Player.Id.ShouldBe(oldPlayer.Id);
        }
    }
}