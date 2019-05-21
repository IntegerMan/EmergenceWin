namespace MattEland.Emergence.Engine.Level.Generation.Prefabs
{
    /// <summary>
    /// Represents an instruction to add a prefab to a level at a specific spot in the level with some supporting properties.
    /// </summary>
    public sealed class LevelAssemblyInstruction
    {

        /// <summary>
        /// Gets or sets the encounter set associated with this prefab.
        /// </summary>
        /// <value>The encounter set.</value>
        public string EncounterSet { get; set; }

        /// <summary>
        /// Gets or sets the prefab identifier.
        /// </summary>
        /// <value>The prefab identifier.</value>
        public string PrefabId { get; set; }

        /// <summary>
        /// Gets or sets the X position within the level.
        /// </summary>
        /// <value>The X position within the level.</value>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y position within the level.
        /// </summary>
        /// <value>The Y position within the level.</value>
        public int Y { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the X axis of the prefab will be flipped.
        /// </summary>
        /// <value><c>true</c> if the X axis should be flipped; otherwise, <c>false</c>.</value>
        public bool FlipX { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the Y axis of the prefab will be flipped.
        /// </summary>
        /// <value><c>true</c> if the Y axis should be flipped; otherwise, <c>false</c>.</value>
        public bool FlipY { get; set; }

    }
}