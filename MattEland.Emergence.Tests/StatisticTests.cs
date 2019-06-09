using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class StatTests
    {
        [TestCase(Actors.Turret, 42)]
        [TestCase(Actors.Helpy, 20)]
        public void AccuracyShouldBeCorrect(string id, decimal expected)
        {
            // Arrange
            var pos = new Pos2D(42, 42);
            
            // Act
            var obj = GameObjectFactory.CreateActor(id, pos);

            // Assert
            obj.EffectiveAccuracy.ShouldBe(expected);
        }

        [TestCase(Actors.Search, 25)]
        [TestCase(Actors.KernelWorker, 15)]
        public void EvasionShouldBeCorrect(string id, decimal expected)
        {
            // Arrange
            var pos = new Pos2D(42, 42);
            
            // Act
            var obj = GameObjectFactory.CreateActor(id, pos);

            // Assert
            obj.EffectiveEvasion.ShouldBe(expected);            
        }

        [TestCase(Actors.LogicBomb, 0)]
        [TestCase(Actors.Virus, 1)]
        public void DefenseShouldBeCorrect(string id, decimal expected)
        {
            // Arrange
            var pos = new Pos2D(42, 42);
            
            // Act
            var obj = GameObjectFactory.CreateActor(id, pos);

            // Assert
            obj.EffectiveDefense.ShouldBe(expected);            
        }
        
        [TestCase(Actors.AntiVirus, 2)]
        [TestCase(Actors.Daemon, 3)]
        public void StrengthShouldBeCorrect(string id, decimal expected)
        {
            // Arrange
            var pos = new Pos2D(42, 42);
            
            // Act
            var obj = GameObjectFactory.CreateActor(id, pos);

            // Assert
            obj.EffectiveStrength.ShouldBe(expected);            
        }
        
    }
}