namespace MattEland.Emergence.Engine.Level
{
    /// <summary>
    /// An enumeration containing a list of valid floor types for cells.
    /// </summary>
    public enum FloorType
    {

        /// <summary>
        /// A normal floor tile
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Represents nothingness. A convenience for cells that do not exist.
        /// </summary>
        Void = 6,

        /// <summary>
        /// A slightly differently decorated floor tile.
        /// </summary>
        DecorativeTile = 3,

        /// <summary>
        /// A decorative walkway.
        /// </summary>
        Walkway = 4,

        /// <summary>
        /// Indicates a caution marker
        /// </summary>
        CautionMarker = 5,
    }
}