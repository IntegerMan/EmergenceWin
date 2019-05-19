namespace MattEland.Emergence.Definitions.DTOs
{
    public enum CommandType
    {
        /// <summary>
        /// Represents a "no action" move.
        /// </summary>
        Wait = 0,

        /// <summary>
        /// Represents a move or melee attack on the tile above the player.
        /// </summary>
        MoveUp = 1,

        /// <summary>
        /// Represents a move or melee attack on the tile to the right of the player.
        /// </summary>
        MoveRight = 2,

        /// <summary>
        /// Represents a move or melee attack on the tile below the player.
        /// </summary>
        MoveDown = 3,

        /// <summary>
        /// Represents a move or melee attack on the tile to the left of the player.
        /// </summary>
        MoveLeft = 4,

        /// <summary>
        /// Represents a request to pathfind towards the specified cell.
        /// </summary>
        Pathfind = 5,

        /// <summary>
        /// Represents a game command of some sort - either activating or deactivating an active command,
        /// executing a targeted command, or executing a non-targeted command.
        /// </summary>
        Command = 6,

    }
}