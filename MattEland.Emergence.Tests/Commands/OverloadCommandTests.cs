using System.Linq;
using MattEland.Emergence.Engine.Commands;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests.Commands
{
    public class OverloadCommandTests : EmergenceTestBase
    {

        [Test]
        public void OverloadShouldDamageNearbyEntities()
        {
            // Arrange
            var potentialTargets = Context.Level.Objects.Where(o => !o.IsPlayer && !o.IsInvulnerable).ToList();
            int originalDamage = potentialTargets.Sum(o => o.MaxStability - o.Stability);

            // Act
            GameService.HandleCommand(new OverloadCommand(), Player.Pos);

            // Assert
            potentialTargets.Sum(o => o.MaxStability - o.Stability).ShouldBeGreaterThan(originalDamage);
        }

        [Test]
        public void OverloadShouldDamageExecutingEntity()
        {
            // Arrange
            int originalStability = Player.Stability;
            Player.EffectiveDefense = 0;

            // Act
            GameService.HandleCommand(new OverloadCommand(), Player.Pos);

            // Assert
            Player.Stability.ShouldBeLessThan(originalStability);
        }
    }
}
