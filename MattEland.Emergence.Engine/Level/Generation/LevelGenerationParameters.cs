namespace MattEland.Emergence.Engine.Level.Generation
{
    /// <summary>
    /// Contains information needed to generate a level
    /// </summary>
    public class LevelGenerationParameters
    {
        /// <summary>
        /// The type of level being generated.
        /// </summary>
        public LevelType LevelType { get; set; }

        public ActorType PlayerType { get; set; } = ActorType.Player;
    }
}
