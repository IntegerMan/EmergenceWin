﻿using System.Linq;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.WpfCore.ViewModels;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.ViewModels
{
    public class GameViewModelTests
    {
        [Test]
        public void GameViewModelShouldStartWithProperState()
        {
            // Arrange

            // Act
            var vm = new GameViewModel();

            // Assert
            vm.VisibleWorldObjects.ShouldNotBeEmpty();
            vm.Commands.ShouldNotBeEmpty();
            vm.Messages.ShouldNotBeEmpty();
            vm.UIState.ShouldBe(UIState.ReadyForInput);
            vm.UIPrompt.ShouldContain("Ready");
        }

        [Test]
        public void GameViewModelShouldHandleCommands()
        {
            // Arrange
            var vm = new GameViewModel();
            var player = vm.VisibleWorldObjects.First(o => o.Source.IsPlayer);
            var oldX = player.X;
            var oldPos = player.Source.Pos;

            // Act
            vm.MovePlayer(MoveDirection.Left);

            // Assert
            player.X.ShouldBe(oldX); // Don't de-center it
            player.Source.Pos.ShouldBe(oldPos.GetNeighbor(MoveDirection.Left));
        }
    }
}
