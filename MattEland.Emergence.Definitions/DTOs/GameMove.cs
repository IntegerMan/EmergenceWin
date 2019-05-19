using System;
using System.Linq;

namespace MattEland.Emergence.Definitions.DTOs
{
    /// <summary>
    /// Represents a player-requested game move.
    /// </summary>
    public class GameMove
    {

        /// <summary>
        /// Gets or sets the unique identifier for the request
        /// </summary>
        public Guid UID { get; set; }

        /// <summary>
        /// Gets or sets the command from the user.
        /// </summary>
        /// <value>The command.</value>
        public GameCommandDTO Command { get; set; }

        /// <summary>
        /// Gets or sets the current state of the game.
        /// </summary>
        /// <value>The state of the game.</value>
        public GameState State { get; set; }

        /// <summary>
        /// Validates this instance and returns a string with the first error encountered, or null if no errors existed.
        /// </summary>
        /// <returns>A validation error or null for no errors.</returns>
        public string Validate()
        {
            if (State == null)
            {
                return "State is required";
            }

            if (Command == null)
            {
                return "Command is required";
            }

            if (State.Level == null)
            {
                return "The level was not included.";
            }

            if (State.Level.Cells == null || !State.Level.Cells.Any())
            {
                return "The level did not contain any cells.";
            }

            if (State.Level.Objects == null || !State.Level.Objects.Any())
            {
                return "The level did not contain any objects.";
            }

            if (State.Level.Player == null)
            {
                return "The player could not be found";
            }

            return null;
        }
    }
}