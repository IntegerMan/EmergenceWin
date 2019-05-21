using System.Collections.Generic;

namespace MattEland.Emergence.Engine.Level.Generation.Encounters
{
    /// <summary>
    /// Represents a set of encounters that might be applied to a prefab.
    /// </summary>
    public class EncounterSet
    {
        /// <summary>
        /// Gets or sets the encounter set identifier.
        /// </summary>
        /// <value>The encounter set identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the identifiers of valid encounters within this encounter set.
        /// </summary>
        /// <value>The encounter ids.</value>
        public IEnumerable<string> EncounterIds { get; set; } = new List<string>();
    }
}