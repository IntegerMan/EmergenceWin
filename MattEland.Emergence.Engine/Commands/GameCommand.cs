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
    public abstract class GameCommand : IGameCommand
    {
        /// <inheritdoc />
        public abstract string Id { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public virtual string ShortName => Name;

        /// <inheritdoc />
        public abstract string Description { get; }

        /// <inheritdoc />
        public virtual CommandActivationType ActivationType => CommandActivationType.Simple;

        /// <inheritdoc />
        public abstract int ActivationCost { get; }

        /// <inheritdoc />
        public virtual int MaintenanceCost { get; } = 1;

        /// <inheritdoc />
        public abstract string IconId { get; }

        public abstract Rarity Rarity { get; }

        public virtual LevelType? MinLevel => null;

        /// <inheritdoc />
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
