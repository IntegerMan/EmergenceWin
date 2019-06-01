using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// Represents basic parameters needed to start a new game.
    /// </summary>
    public class NewGameParameters
    {
        public ActorType PlayerType { get; set; } = ActorType.Player;

    }
}