using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CombatTests
    {
        private GameService _gameService;
        private CommandContext _context;
        private Player _attacker;
        private GameObjectBase _defender;
        private const string Verb = "whacks";

        [SetUp]
        public void Initialize()
        {
            _gameService = new GameService();
            _context = _gameService.StartNewGame();
            _attacker = _gameService.Player;

            _defender = CreateTurret();

            _context.ClearMessages();
        }

        [Test]
        public void HandleHit()
        {
            // Arrange
            _attacker.Accuracy = 100;
            _attacker.EffectiveStrength = 3;
            _attacker.EffectiveAccuracy = 100;
            _defender.EffectiveEvasion = 0;
            _defender.EffectiveDefense = 0;

            // Act
            _context.CombatManager.HandleAttack(_context, _attacker, _defender, Verb, DamageType.Normal);

            // Assert
            _context.Messages.ShouldNotBeEmpty();
            _defender.Stability.ShouldBeLessThan(_defender.MaxStability);
        }

        [Test]
        public void NonLethalShouldGenerateExpectedMessages()
        {
            // Arrange
            var damage = 1;

            // Act
            var message = _context.CombatManager.HurtObject(_context, _attacker, _defender, damage, Verb, DamageType.Normal);

            // Assert
            message.ShouldBe($"{_attacker.Name} {Verb} {_defender.Name} for {damage} Damage");
        }

        [Test]
        public void LethalShouldGenerateExpectedMessages()
        {
            // Arrange
            var damage = _defender.Stability;

            // Act
            var message = _context.CombatManager.HurtObject(_context, _attacker, _defender, damage, Verb, DamageType.Normal);

            // Assert
            message.ShouldBe($"{_attacker.Name} {Verb} {_defender.Name} for {damage} Damage, terminating it");
        }

        private static GameObjectBase CreateTurret()
        {
            return GameObjectFactory.CreateFromObjectType("ACTOR_TURRET", GameObjectType.Actor, new Pos2D(-12, 42));
        }
    }
}