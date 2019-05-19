using MattEland.Emergence.Definitions.DTOs;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Definitions.Commands
{
    public interface IGameCommand
    {
        /// <summary>
        /// The unique Identifier of the command
        /// </summary>
        string Id { get; }

        /// <summary>
        /// The full name of the command.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// An abbreviated version of the command's name, for use in a toolbar.
        /// </summary>
        string ShortName { get; }

        /// <summary>
        /// A detailed description of the command suitable for a details view or tooltip.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The type of command activation this command follows.
        /// </summary>
        CommandActivationType ActivationType { get; }

        /// <summary>
        /// The cost to use a command or to switch an active command on.
        /// </summary>
        int ActivationCost { get; }

        /// <summary>
        /// The per-turn cost to keep an active command active.
        /// </summary>
        int MaintenanceCost { get; }

        string IconId { get; }
        Rarity Rarity { get; }
        LevelType? MinLevel { get; }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="context">The current command context.</param>
        /// <param name="executor">The actor executing the command.</param>
        /// <param name="pos">The targeted position for the command. For non-targeted commands, this will be <paramref name="executor"/>'s current position.</param>
        bool Execute(ICommandContext context, IActor executor, Pos2D pos, bool isCurrentlyActive);

        /// <summary>
        /// Builds a data transmission object based on this instance.
        /// </summary>
        /// <returns>An equivalent data transmission object</returns>
        CommandInfoDto BuildDto(bool isActive);

        /// <summary>
        /// Applies the effect of the command given the specified parameters. This method does not need to be
        /// concerned about operations costs.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="executor">The actor executing the command.</param>
        /// <param name="pos">The command's targeted position, or <paramref name="executor"/>'s position.</param>
        void ApplyEffect(ICommandContext context, IActor executor, Pos2D pos);

        void ApplyPreActionEffect(ICommandContext context, IActor executor, Pos2D playerPosition);
    }
}