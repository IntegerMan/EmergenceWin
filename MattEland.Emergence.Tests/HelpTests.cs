using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class HelpTests : EmergenceTestBase
    {
        [Theory]
        [TestCase(0, "help_firewall")]
        [TestCase(1, "help_welcome")]
        [TestCase(2, "help_firewall")]
        [TestCase(3, "help_welcome")]
        public void DisplayStandardHelpShouldWork(int corruption, string topic)
        {
            // Arrange
            var source = GetHelpTile();
            source.Corruption = corruption;

            // Act
            Context.DisplayHelp(source, topic);

            // Assert
            Context.Messages.ShouldContain(c => c is HelpTextEffect);
        }

        [Test]
        public void DisplayHelpOnActorShouldWork()
        {
            // Arrange
            var source = GetHelpTile();

            // Act
            Context.DisplayHelp(source, $"help_{Actors.PlayerAntiVirus}");

            // Assert
            Context.Messages.ShouldContain(c => c is HelpTextEffect);
        }

        [NotNull]
        private HelpTile GetHelpTile()
        {
            return Context.Level.Objects.OfType<HelpTile>().First();
        }
    }
}