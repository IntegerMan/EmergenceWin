namespace MattEland.Emergence.Engine.Commands
{
    /// <summary>
    /// An enum defining various types of game commands.
    /// </summary>
    public enum CommandActivationType
    {
        /// <summary>
        /// Represents a simple command that does not require a target and has a one-time activation (is not an active command)
        /// </summary>
        Simple,
        /// <summary>
        /// Represents a command that takes in a targeted cell and executes once.
        /// </summary>
        Targeted,
        /// <summary>
        /// Represents a command that activates and provides a constant effect once on.
        /// It can be toggled on and off at will and typically costs a certain amount of per-turn maintenance.
        /// </summary>
        Active
    }
}