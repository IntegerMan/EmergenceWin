using System.Linq;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using NUnit.Framework;
using Shouldly;

namespace MattEland.Emergence.Tests
{
    public class HelpTests : EmergenceTestBase
    {
        [Theory]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DisplayStandardHelpShouldWork(int corruption)
        {
            // Arrange
            var source = Context.Level.Objects.OfType<HelpTile>().First();
            source.Corruption = corruption;

            // Act
            Context.DisplayHelp(source, "help_welcome");

            // Assert
            Context.Messages.ShouldContain(c => c is HelpTextEffect);
        }
    }
}