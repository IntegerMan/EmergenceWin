using System.Runtime.Remoting.Contexts;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.AI;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class BehaviorTests : EmergenceTestBase
    {

        [NotNull]
        protected ArtificialIntelligenceService AI => Context.AI;

        protected CommonBehaviors Behaviors => AI.CommonBehaviors;

        [Test]
        public void BehaviorSystemShouldNotEvaluateForPlayer()
        {
            // Arrange
            var pos = Player.Pos;

            // Act
            var result = AI.ProcessActorTurn(Player);

            // Assert
            result.SelectedBehavior.ShouldBeNull();
            Player.Pos.ShouldBe(pos);
        }

        [Test] 
        public void BehaviorSystemShouldReturnIdleForTurret()
        {
            // Arrange
            var turretPos = Player.Pos.GetNeighbor(MoveDirection.Left, 2);
            var turret = (Turret)GameObjectFactory.CreateActor(Actors.Turret, turretPos);

            // Act
            var result = AI.ProcessActorTurn(turret);

            // Assert
            result.SelectedBehavior.ShouldBe(Behaviors.Idle);
            result.EvaluatedBehaviors.ShouldNotContain(Behaviors.Wander);
            turret.Pos.ShouldBe(turretPos);
        }

        [Test] 
        public void BehaviorSystemShouldEvaluateIdleForHelpy()
        {
            // Arrange
            var turretPos = Player.Pos.GetNeighbor(MoveDirection.Left, 10);
            var actor = (Actor)GameObjectFactory.CreateActor(Actors.Helpy, turretPos);

            // Act
            var result = AI.ProcessActorTurn(actor);

            // Assert
            result.SelectedBehavior.ShouldBe(Behaviors.Wander);
        }
    }
}