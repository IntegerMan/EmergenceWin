using System.Collections.Generic;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Level
{
    /// <summary>
    /// Represents an entity type within the game world.
    /// </summary>
    public sealed class EntityData
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        public bool BlocksSight { get; set; }
        public int HP { get; set; }
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int Strength { get; set; }
        public int Defense { get; set; }
        public decimal LineOfSightRadius { get; set; }
        public Alignment Team { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Commands { get; set; }
        public int OP { get; set; }
        public bool IsImmobile { get; set; }
        public string HelpText { get; set; }
        public Rarity LootRarity { get; set; }
    }
}