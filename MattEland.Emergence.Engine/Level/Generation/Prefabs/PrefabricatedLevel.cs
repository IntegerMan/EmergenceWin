using System.Collections.Generic;

namespace MattEland.Emergence.Engine.Level.Generation.Prefabs
{
    /// <summary>
    /// Represents a level composed of prefabricated rooms.
    /// </summary>
    public sealed class PrefabricatedLevel
    {
        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the player's start position within the level.
        /// </summary>
        /// <value>The player's start position.</value>
        public Pos2D Start { get; set; }

        /// <summary>
        /// Gets or sets the set of level assembly instructions used to knit together prefabs into a level structure.
        /// </summary>
        /// <value>The instructions.</value>
        public IEnumerable<LevelAssemblyInstruction> Instructions { get; set; }
    }
}