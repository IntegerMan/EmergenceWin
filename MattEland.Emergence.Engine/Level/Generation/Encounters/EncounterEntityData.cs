namespace MattEland.Emergence.Engine.Level.Generation.Encounters
{
    /// <summary>
    /// Represents an entity type that may be present in an encounter. There may be from 0 to many entities present for an encounter.
    /// </summary>
    public sealed class EncounterEntityData
    {
        /// <summary>
        /// Gets or sets the entity identifier.
        /// </summary>
        /// <value>The entity identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the chance of the entity appearing with 0 being no chance whatsoever and 1.0 being certain.
        /// </summary>
        /// <value>The chance of appearance from 0.0 to 1.0.</value>
        public decimal Chance { get; set; }

        /// <summary>
        /// Gets or sets the minimum quantity of entities that will be encountered.
        /// </summary>
        /// <value>The minimum quantity.</value>
        public int MinQuantity { get; set; } = 1;

        /// <summary>
        /// Gets or sets the maximum quantity of entities that will be encountered.
        /// </summary>
        /// <value>The maximum quantity.</value>
        public int MaxQuantity { get; set; } = 1;
    }
}