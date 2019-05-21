using System.Collections.Generic;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// A data transmission object used for serializing level data between the client and the server
    /// </summary>
    public class LevelDto
    {
        /// <summary>
        /// Gets or sets the level identifier.
        /// </summary>
        /// <value>The level identifier.</value>
        public LevelType Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        /// <value>The name of the level.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the upper left corner of the level. This can potentially include negative X / Y's.
        /// </summary>
        /// <value>The upper left corner.</value>
        public string UpperLeft { get; set; }

        /// <summary>
        /// Gets or sets the lower right corner of the level. This can potentially include negative X / Y's
        /// </summary>
        /// <value>The lower right corner.</value>
        public string LowerRight { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        /// <value>The player.</value>
        public PlayerDto Player { get; set; }

        public IEnumerable<ActorDto> Actors { get; set; } = new List<ActorDto>();

        public IEnumerable<GameObjectDto> Objects { get; set; } = new List<GameObjectDto>();
        public IEnumerable<GameObjectDto> Walls { get; set; } = new List<GameObjectDto>();

        public IEnumerable<string> Cells { get; set; } = new List<string>();
        public IEnumerable<string> Corruption { get; set; } = new List<string>();

        /// <summary>
        /// Gets or sets the collection of openable objects. Typically these are doors or treasure chests or the like.
        /// </summary>
        /// <value>The openable objects.</value>
        public IEnumerable<OpenableDto> Openable { get; set; } = new List<OpenableDto>();

        public bool HasAdminAccess { get; set; }

        /// <summary>
        /// Gets or sets the location marked by the Mark command within this level.
        /// If the Mark command has not been executed, this will be the level start.
        /// </summary>
        public string MarkedPos { get; set; }
    }
}