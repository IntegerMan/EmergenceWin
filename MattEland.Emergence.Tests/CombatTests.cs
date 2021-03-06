﻿using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CombatTests : EmergenceTestBase
    {
        public GameObjectBase Turret { get; private set; }
        private const string Verb = "whacks";

        protected override void Initialize()
        {
            base.Initialize();

            Turret = CreateTurret(new Pos2D(-12, 42));
        }

        [Test]
        public void HandleHit()
        {
            // Arrange
            Player.EffectiveStrength = 3;
            Player.EffectiveAccuracy = 100;
            Turret.EffectiveEvasion = 0;
            Turret.EffectiveDefense = 0;

            // Act
            Context.CombatManager.HandleAttack(Context, Player, Turret, Verb, DamageType.Normal);

            // Assert
            Context.Messages.ShouldNotBeEmpty();
            Turret.Stability.ShouldBeLessThan(Turret.MaxStability);
        }

        [Test]
        public void NonLethalShouldGenerateExpectedMessages()
        {
            // Arrange
            var damage = 1;

            // Act
            var message = Context.CombatManager.HurtObject(Context, Player, Turret, damage, Verb, DamageType.Normal);

            // Assert
            message.ShouldBe($"{Player.Name} {Verb} {Turret.Name} for {damage} Damage");
        }

        [Test]
        public void LethalShouldGenerateExpectedMessages()
        {
            // Arrange
            var damage = Turret.Stability;

            // Act
            var message = Context.CombatManager.HurtObject(Context, Player, Turret, damage, Verb, DamageType.Normal);

            // Assert
            message.ShouldBe($"{Player.Name} {Verb} {Turret.Name} for {damage} Damage, terminating it");
        }

        [Test]
        public void AttackingAWeakActorShouldKillIt()
        {
            // Arrange
            var pos = Player.Pos.GetNeighbor(MoveDirection.Left);
            var target = AddObject(GameObjectFactory.CreateActor(Actors.Bit, pos));
            Player.EffectiveAccuracy = 100;
            target.EffectiveEvasion = 0;
            target.EffectiveDefense = 0;
            target.Stability = 1;

            // Act
            GameService.MovePlayer(MoveDirection.Left);

            // Assert
            target.IsDead.ShouldBeTrue();
        }
        
        [Test]
        public void AttackingAWeakActorShouldNotEnterItsCell()
        {
            // Arrange
            var oldPos = Player.Pos;
            var pos = Player.Pos.GetNeighbor(MoveDirection.Left);
            Context.AddObject(GameObjectFactory.CreateActor(Actors.Bit, pos));

            // Act
            GameService.MovePlayer(MoveDirection.Left);

            // Assert
            Player.Pos.ShouldBe(oldPos);
        }

    }
}