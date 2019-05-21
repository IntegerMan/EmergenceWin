using MattEland.Emergence.Engine.Commands;

namespace MattEland.Emergence.Engine.DTOs
{
    /// <summary>
    /// Represents full information about a command that a client application might need to know about.
    /// </summary>
    public class CommandInfoDto
    {

        /// <summary>
        /// The unique Identifier of the command
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The full name of the command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the key of a unique client-side icon reference potentially used to display
        /// the icon on the user interface.
        /// </summary>
        public string IconId { get; set; }

        /// <summary>
        /// An abbreviated version of the command's name, for use in a toolbar.
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// A detailed description of the command suitable for a details view or tooltip.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The type of command activation this command follows.
        /// </summary>
        public CommandActivationType ActivationType { get; set; }

        /// <summary>
        /// The cost to use a command or to switch an active command on.
        /// </summary>
        public int ActivationCost { get; set; }

        /// <summary>
        /// The per-turn cost to keep an active command active.
        /// </summary>
        public int MaintenanceCost { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the command is currently activate. This is only applicable to active commands.
        /// </summary>
        public bool IsActive { get; set; }

    }
}