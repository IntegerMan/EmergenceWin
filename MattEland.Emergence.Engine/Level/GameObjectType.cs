namespace MattEland.Emergence.Engine.Level
{
    public enum GameObjectType
    {
        /// <summary>
        /// Represents a level core, needed to progress to the next level
        /// </summary>
        Core = 0,
        /// <summary>
        /// A transparent divider that blocks movement
        /// </summary>
        Divider = 1,
        /// <summary>
        /// Floor-based cabling
        /// </summary>
        Cabling = 2,
        /// <summary>
        /// A turret that can fire at nearby targets
        /// </summary>
        Turret = 3,
        /// <summary>
        /// A firewall, blocking access until Cores are taken down
        /// </summary>
        Firewall = 4,
        /// <summary>
        /// The level exit
        /// </summary>
        Exit = 5,
        /// <summary>
        /// The level entrance
        /// </summary>
        Entrance = 6,
        /// <summary>
        /// A service within the level
        /// </summary>
        Service = 7,
        /// <summary>
        /// A data storage device
        /// </summary>
        DataStore = 8,
        /// <summary>
        /// A wall within the level
        /// </summary>
        Wall = 9,
        /// <summary>
        /// Debris from a destroyed object
        /// </summary>
        Debris = 10,
        /// <summary>
        /// A doorway.
        /// </summary>
        Door = 11,
        /// <summary>
        /// Freestanding loot to be collected
        /// </summary>
        CommandPickup = 12,
        /// <summary>
        /// A treasure chest
        /// </summary>
        Treasure = 13,
        /// <summary>
        /// A water tile
        /// </summary>
        Water = 14,
        /// <summary>
        /// Some form of mobile object inside of the game engine
        /// </summary>
        Actor = 15,
        /// <summary>
        /// Represents the player within the game world.
        /// </summary>
        Player = 16,
        /// <summary>
        /// A block used to display specific help information when triggered.
        /// </summary>
        Help = 17,
        /// <summary>
        /// A block used to select a specific character on the character selection screen.
        /// </summary>
        CharacterSelect = 18,
        GenericPickup = 19,
        Floor
    }
}