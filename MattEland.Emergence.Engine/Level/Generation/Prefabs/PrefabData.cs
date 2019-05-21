using System.Collections.Generic;
using System.ComponentModel;

namespace MattEland.Emergence.Engine.Level.Generation.Prefabs
{
    /// <summary>
    /// Represents a prefabricated room in the game world.
    /// </summary>
    public sealed class PrefabData
    {
        /// <summary>
        /// Gets the room identifier.
        /// </summary>
        /// <value>The room identifier.</value>
        public string Id { get; set;  }

        /// <summary>
        /// Gets the data for the room's cells. This is an array of strings with each string representing
        /// a series of cells with individual characters describing a single cell.
        /// </summary>
        /// <value>The prefab data array.</value>
        public IList<string> Data { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the minimum number of cores that should be generated in the room.
        /// </summary>
        /// <value>The minimum number of cores that should be generated in the room.</value>
        [DefaultValue(0)]
        public int MinCores { get; set; } = 0;

        /// <summary>
        /// Gets or sets the maximum number of cores that should be generated in the room.
        /// </summary>
        /// <value>The maximum number of cores that should be generated in the room.</value>
        [DefaultValue(0)]
        public int MaxCores { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value indicating whether this prefabs walls will be invulnerable. This is useful for prefabs representing level or game end conditions.
        /// </summary>
        /// <value><c>true</c> if this prefab's walls should be invulnerable; otherwise, <c>false</c>.</value>
        [DefaultValue(false)]
        public bool IsInvulnerable { get; set; }

        public IList<PrefabMapping> Mapping { get; set; }
    }
}