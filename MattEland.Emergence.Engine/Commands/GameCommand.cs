using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    /// <summary>
    /// An abstract class containing basic functionality around executing various types of game commands.
    /// </summary>
    public abstract class GameCommand
    {
        /// <summary>
        /// The unique Identifier of the command
        /// </summary>
        public abstract string Id { get; }

        /// <summary>
        /// The full name of the command.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// An abbreviated version of the command's name, for use in a toolbar.
        /// </summary>
        public virtual string ShortName => Name;

        /// <summary>
        /// A detailed description of the command suitable for a details view or tooltip.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// The type of command activation this command follows.
        /// </summary>
        public virtual CommandActivationType ActivationType => CommandActivationType.Simple;

        /// <summary>
        /// The cost to use a command or to switch an active command on.
        /// </summary>
        public abstract int ActivationCost { get; }

        /// <summary>
        /// The per-turn cost to keep an active command active.
        /// </summary>
        public virtual int MaintenanceCost { get; } = 1;

        public abstract string IconId { get; }

        public abstract Rarity Rarity { get; }

        public virtual LevelType? MinLevel => null;

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="context">The current command context.</param>
        /// <param name="executor">The actor executing the command.</param>
        /// <param name="pos">The targeted position for the command. For non-targeted commands, this will be <paramref name="executor"/>'s current position.</param>
        public bool Execute(CommandContext context, Actor executor, Pos2D pos, bool isCurrentlyActive)
        {
            if (ActivationType == CommandActivationType.Active && isCurrentlyActive)
            {
                if (executor.IsPlayer)
                {
                    context.AddMessage($"{Name} deactivated", ClientMessageType.Generic);
                    context.AddEffect(new DeactivatedEffect(executor, Name));
                }

                OnDeactivated(context, executor, pos);

                return false;
            }

            // Don't let the player target things they can't see
            if (ActivationType == CommandActivationType.Targeted && !executor.CanSee(pos))
            {
                if (executor.IsPlayer)
                {
                    context.AddMessage($"{executor.Name} tries to {Name} but cannot see the target",
                                       ClientMessageType.Failure);
                }

                return false;
            }

            if (executor.Operations >= ActivationCost)
            {
                executor.Operations -= ActivationCost;

                if (ActivationType == CommandActivationType.Active)
                {
                    if (executor.IsPlayer)
                    {
                        context.AddMessage($"{Name} activated", ClientMessageType.Generic);
                        context.AddEffect(new ActivatedEffect(executor, Name));
                    }

                    OnActivated(context, executor, pos);
                }
                else
                {
                    context.AddEffect(new TauntEffect(executor, $"{Name}!"));
                    ApplyEffect(context, executor, pos);
                }

                return ActivationType == CommandActivationType.Active;
            }

            if (executor.IsPlayer)
            {
                string opsPluralString = ActivationCost == 1 ? "operation" : "operations";
                string message = executor.IsPlayer
                    ? $"You must have {ActivationCost} free {opsPluralString} to use {Name}"
                    : $"{executor.Name} tries to use {Name} but doesn't have enough free operations";

                context.AddMessage(message, ClientMessageType.Failure);
            }

            return false;
        }

        protected virtual void OnActivated(CommandContext context, Actor executor, Pos2D pos)
        {
        }

        protected virtual void OnDeactivated(CommandContext context, Actor executor, Pos2D pos)
        {
        }

        /// <summary>
        /// Applies the effect of the command given the specified parameters. This method does not need to be
        /// concerned about operations costs.
        /// </summary>
        /// <param name="context">The command context.</param>
        /// <param name="executor">The actor executing the command.</param>
        /// <param name="pos">The command's targeted position, or <paramref name="executor"/>'s position.</param>
        public abstract void ApplyEffect(CommandContext context, Actor executor, Pos2D pos);


        public virtual void ApplyPreActionEffect(CommandContext context, Actor executor, Pos2D pos)
        {

        }

        /// <summary>
        /// Builds a data transmission object based on this instance.
        /// </summary>
        /// <returns>An equivalent data transmission object</returns>
        public CommandInfoDto BuildDto(bool isActive) => new CommandInfoDto
        {
            Id = Id,
            IconId = IconId,
            Name = Name,
            ShortName = ShortName,
            ActivationCost = ActivationCost,
            ActivationType = ActivationType,
            MaintenanceCost = MaintenanceCost,
            Description = Description,
            IsActive = isActive
        };
    }
}
