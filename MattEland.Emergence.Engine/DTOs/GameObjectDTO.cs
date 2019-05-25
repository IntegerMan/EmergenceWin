using System.Diagnostics;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// A data transmission object used for representing an object within the game world.
    /// </summary>
    [DebuggerDisplay("(GameObject: Pos:{Pos.SerializedValue} Type:{Type} Id:{ObjectId})")]
    public class GameObjectDto
    {
        /// <summary>
        /// Gets the position of the object within the game world.
        /// </summary>
        /// <value>The position of the object.</value>
        public string Pos { get; set; }

        /// <summary>
        /// Gets the type of the game object.
        /// </summary>
        /// <value>The type of the object.</value>
        public GameObjectType Type { get; set; }

        /// <summary>
        /// Gets or sets the object identifier.
        /// </summary>
        /// <value>The object identifier.</value>
        public string ObjectId { get; set; }

        /// <summary>
        /// Gets or sets the health lost from the object's maximum health. Typically this will be 0 until something is hurt,
        /// and we don't serialize 0 values, so this is a minor performance hack.
        /// </summary>
        /// <value>The health lost of the object.</value>
        public int HpUsed { get; set; }

        /// <summary>
        /// Gets or sets the maximum health of the object.
        /// </summary>
        /// <value>The maximum health.</value>
        public int MaxHp { get; set; }

        /// <summary>
        /// Gets or sets the name of the object
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents which team the object is on.
        /// </summary>
        public Alignment Team { get; set; }

        /// <summary>
        /// Represents a high-level state of the object.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Represents whether or not the object should be explicitly hidden on the client-side
        /// </summary>
        public bool IsHidden { get; set; }

        /// <summary>
        /// Gets or sets the corruption amount present on the object.
        /// </summary>
        public int Corruption { get; set; }
    }
}