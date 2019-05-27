using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Collections;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class ExplosionTests : EmergenceTestBase
    {

        [Test]
        public void ExplosionShouldDamageNearbyObjects()
        {
            // Arrange
            var radius = 5;
            var pos = Player.Pos.GetNeighbor(MoveDirection.Right);
            var objects = new List<GameObjectBase>();
            Context.Level.GetCellsInSquare(pos, radius - 2).Each(c => c.Objects.Each(o => objects.Add(o)));
            objects.Each(o => o.EffectiveDefense = 0);

            // Act
            Context.CombatManager.HandleExplosion(Context, Player, pos, 3, radius, DamageType.Normal);

            // Assert
            objects.Where(o => !o.IsInvulnerable).Each(o => o.Stability.ShouldBeLessThan(o.MaxStability));
        }

    }
}
