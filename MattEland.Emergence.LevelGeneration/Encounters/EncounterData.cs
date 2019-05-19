using System.Collections.Generic;

namespace MattEland.Emergence.LevelGeneration.Encounters
{
    /// <summary>
    /// An encounter template that can be applied to a room in order to seed the room with a pre-defined set of contents. 
    /// </summary>
    public sealed class EncounterData
    {
        /// <summary>
        /// Gets or sets the identifier of the encounter.
        /// </summary>
        /// <value>The encounter identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the difficulty of the encounter.
        /// </summary>
        /// <value>The difficulty.</value>
        public EncounterDifficulty Difficulty { get; set; }

        /// <summary>
        /// Gets the entity types that may be part of the encounter.
        /// </summary>
        /// <value>The entities.</value>
        public IEnumerable<EncounterEntityData> Entities { get; } = new List<EncounterEntityData>();
    }
}