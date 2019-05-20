using MattEland.Emergence.Definitions.Level;

namespace MattEland.Emergence.Definitions.DTOs
{
    /// <summary>
    /// Represents a command the player sends to the game engine.
    /// </summary>
    public class GameCommandDTO
    {
        /// <summary>
        /// Gets or sets the type of command being executed.
        /// </summary>
        /// <value>The command type.</value>
        public CommandType CommandType { get; set; }

        /// <summary>
        /// Gets or sets the position associated with the command, if one is present.
        /// </summary>
        /// <value>The command position.</value>
        public string CommandPosition { get; set; }

        /// <summary>
        /// Gets or sets additional command information related to the current command.
        /// </summary>
        /// <value>The command information.</value>
        public string CommandData { get; set; }

        public Pos2D CalculateRequestedNewPosition(IGameObject player)
        {
            switch (CommandType)
            {
                case CommandType.MoveUp:
                    return player.Pos.Add(0, -1);

                case CommandType.MoveDown:
                    return player.Pos.Add(0, 1);

                case CommandType.MoveRight:
                    return player.Pos.Add(1, 0);

                case CommandType.MoveLeft:
                    return player.Pos.Add(-1, 0);

                default:
                    return player.Pos;
            }
        }

    }
}