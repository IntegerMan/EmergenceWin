namespace MattEland.Emergence.Engine.Level
{
    /// <summary>
    /// An enum identifying the types of levels in the game world.
    /// </summary>
    public enum LevelType
    {
        /// <summary>
        /// The tutorial level. A short, pre-scripted level to teach basic game concepts.
        /// Not yet implemented.
        /// </summary>
        Tutorial = 0,

        /// <summary>
        /// A client workstation. This is an average everyday computer.
        /// </summary>
        ClientWorkstation = 1,

        /// <summary>
        /// A smart fridge or other IoT device.
        /// </summary>
        SmartFridge = 2,

        /// <summary>
        /// A busy messaging server carrying out day-to-day operations.
        /// </summary>
        MessagingServer = 3,

        /// <summary>
        /// A bastion server. Expect heavy resistance.
        /// </summary>
        Bastion = 4,

        /*
        /// <summary>
        /// A honeypot server in the demilitarized zone in the network. Expect insane security. 
        /// </summary>
        DemilitarizedZone = 5,
        */

        /// <summary>
        /// The router's gateway. The final level of the game.
        /// </summary>
        RouterGateway = 6,
        Escaped = 100,

        /// <summary>
        /// A simple door testing level
        /// </summary>
        TestDoors = 101,
        Training = 102,
        TrainSingleRoom = 103,
        TrainTwinRooms = 104,
        TrainFriendlyFire = 105
    }
}