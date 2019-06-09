using MattEland.Emergence.Engine.Entities.Actors;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// Represents basic parameters needed to start a new game.
    /// </summary>
    public class NewGameParameters
    {
        public PlayerType PlayerType { get; set; } = PlayerType.Logistics;

    }
}