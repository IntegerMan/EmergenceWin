using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Services;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class CombatTests : EmergenceTestBase
    {
        public GameObjectBase Turret { get; private set; }
        private const string Verb = "whacks";

        [SetUp]
        public void Initialize()
        {
            InitializeGameService();

            Turret = CreateTurret();

            Context.ClearMessages();
        }

        [Test]
        public void HandleHit()
        {
            // Arrange
            Player.Accuracy = 100;
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
    }
}