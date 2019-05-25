using System;
using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// Represents the current state of the game world. This contains everything necessary to represent the game on a
    /// turn-by-turn basis and should be passed between the client and the server with modifications every move.
    /// </summary>
    public class GameState
    {

        /// <summary>
        /// Gets the version identifier of the game state object. This is used for migration purposes.
        /// </summary>
        [UsedImplicitly] public string Version => "0.4.1";

        /// <summary>
        /// Gets or sets the number of moves made.
        /// </summary>
        /// <value>The number of moves made.</value>
        public int NumMoves { get; set; }

        /// <summary>
        /// Gets or sets the level data.
        /// </summary>
        /// <value>The level data.</value>
        public LevelDto Level { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the game has ended.
        /// </summary>
        /// <value><c>true</c> if the game had ended; otherwise, <c>false</c>.</value>
        public bool IsGameOver { get; set; }

        /// <summary>
        /// If true, and if IsGameOver is true, the game has ended in a player victory
        /// </summary>
        public bool IsVictory { get; set; }

        /// <summary>
        /// Tracks the player's total score.
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// The random number seed used for randomization
        /// </summary>
        public int Seed { get; set; }

        /// <summary>
        /// Gets the unique identifier for the game
        /// </summary>
        public Guid Uid { get; set; }
    }
}