using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Loot;
using MattEland.Emergence.Engine.Services;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CombatTests
    {
        private const string Verb = "whacks";

        [Test]
        public void NonLethalShouldGenerateExpectedMessages()
        {
            // Arrange
            var gameService = new GameService();
            var context = gameService.StartNewGame();
            var attacker = gameService.Player;
            var defender = CreateTurret();
            var damage = 1;

            // Act
            var message = context.CombatManager.HurtObject(context, attacker, defender, damage, Verb, DamageType.Normal);

            // Assert
            message.ShouldBe($"{attacker.Name} {Verb} {defender.Name} for {damage} Damage");
        }

        [Test]
        public void LethalShouldGenerateExpectedMessages()
        {
            // Arrange
            var gameService = new GameService();
            var context = gameService.StartNewGame();
            var attacker = gameService.Player;
            var defender = CreateTurret();
            var damage = defender.Stability;

            // Act
            var message = context.CombatManager.HurtObject(context, attacker, defender, damage, Verb, DamageType.Normal);

            // Assert
            message.ShouldBe($"{attacker.Name} {Verb} {defender.Name} for {damage} Damage, terminating it");
        }

        private static GameObjectBase CreateTurret()
        {
            return GameObjectFactory.CreateFromObjectType("ACTOR_TURRET", GameObjectType.Actor, new Pos2D(-12, 42));
        }
    }
}