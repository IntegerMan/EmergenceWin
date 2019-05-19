using System.ComponentModel.DataAnnotations;
using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.LevelGeneration
{
    /// <summary>
    /// Contains information needed to generate a level
    /// </summary>
    public class LevelGenerationParameters
    {
        /// <summary>
        /// The type of level being generated.
        /// </summary>
        [Required]
        public LevelType LevelType { get; set; }

        public string PlayerId { get; set; } = "ACTOR_PLAYER_LOGISTICS";
    }
}
