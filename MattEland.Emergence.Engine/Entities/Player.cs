﻿using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Entities
{
    public class Player : Actor
    {
        public Player(PlayerDto dto) : base(dto)
        {
            HotbarCommands = new List<CommandInstance>();
            CreateCommandReferences(dto.Hotbar, HotbarCommands);

            StoredCommands = new List<CommandInstance>();
            CreateCommandReferences(dto.StoredCommands, StoredCommands);
        }

        private static void CreateCommandReferences([CanBeNull] IEnumerable<CommandInfoDto> commandDtos,
            [NotNull] ICollection<CommandInstance> commandRefCollection)
        {
            if (commandDtos == null) return;

            foreach (var commandInfoDto in commandDtos)
            {
                CommandInstance reference = null;

                if (commandInfoDto != null)
                {
                    reference = CommandFactory.CreateCommandReference(commandInfoDto);
                }

                commandRefCollection.Add(reference);
            }
        }

        public override bool HasAi => false;

        [ItemCanBeNull] public IList<CommandInstance> HotbarCommands { get; }

        [ItemCanBeNull] public IList<CommandInstance> StoredCommands { get; }

        [ItemNotNull]
        public IEnumerable<CommandInstance> Commands
        {
            get
            {
                foreach (var commandInstance in HotbarCommands)
                {
                    if (commandInstance?.Command != null)
                    {
                        yield return commandInstance;
                    }
                }

                foreach (var commandInstance in StoredCommands)
                {
                    if (commandInstance?.Command != null)
                    {
                        yield return commandInstance;
                    }
                }
            }
        }

        public bool AttemptPickupItem(CommandContext context, GameObjectBase item)
        {
            var command = CommandFactory.CreateCommand(item.ObjectId);

            if (item.IsCorrupted)
            {
                context.AddMessage($"{Name} picks up the corrupted item and is struck by a virus!", ClientMessageType.Failure);
                var message = context.CombatManager.HurtObject(context, item, this, 3, "infects", DamageType.Combination);
                context.AddMessage(message, ClientMessageType.Failure);
                return true;
            }

            if (command == null)
            {
                context.AddError($"{Name} attempts to pick up the command but it vanishes into the void");
                return true;
            }

            var dto = command.BuildDto(false);

            int index = HotbarCommands.IndexOf(null);
            if (index >= 0)
            {
                HotbarCommands[index] = CommandFactory.CreateCommandReference(dto);
                context.AddMessage($"{Name} picks up {command.Name}", ClientMessageType.Success);
                return true;
            }

            index = StoredCommands.IndexOf(null);
            if (index >= 0)
            {
                StoredCommands[index] = CommandFactory.CreateCommandReference(dto);
                context.AddMessage($"{Name} picks up {command.Name} and stores it", ClientMessageType.Success);
                return true;
            }

            context.AddMessage($"{Name} does not have enough free space to pick up {command.Name}",
                ClientMessageType.Failure);
            return false;
        }

        /// <inheritdoc />
        public override bool IsPlayer => true;

        /// <inheritdoc />
        public override bool IsCommandActive(GameCommand command) =>
            HotbarCommands.Any(c => c != null && c.IsActive && c.Command == command);

        public override void SetCommandActiveState(GameCommand command, bool isActive)
        {
            if (command == null)
            {
                return;
            }

            var match = HotbarCommands.FirstOrDefault(c => c?.Command == command);

            if (match != null)
            {
                match.IsActive = isActive;
            }
        }

        public override void MaintainActiveEffects(CommandContext context)
        {
            base.MaintainActiveEffects(context);

            foreach (var cmd in HotbarCommands.Where(c => c?.Command != null && c.IsActive &&
                                                          c.Command.ActivationType == CommandActivationType.Active))
            {
                // Deactivate the command if it can't be paid for
                if (cmd.Command.MaintenanceCost > Operations)
                {
                    if (IsPlayer)
                    {
                        context.AddMessage($"{cmd.Command.Name} deactivates due to lack of available operations",
                            ClientMessageType.Failure);
                        context.AddEffect(new DeactivatedEffect(this, cmd.Command.Name));
                    }

                    cmd.IsActive = false;
                    continue;
                }

                AdjustOperationsPoints(-cmd.Command.MaintenanceCost);

                cmd.Command.ApplyEffect(context, this, Pos);
            }
        }

        public override void ApplyActiveEffects(CommandContext context)
        {
            HotbarCommands.Where(c => c?.Command != null && 
                                 c.IsActive &&
                                 c.Command.ActivationType == CommandActivationType.Active)
                          .Each(c => c.Command.ApplyPreActionEffect(context, this, Pos));

            if (ObjectId == Actors.AntiVirus)
            {
                CorruptionHelper.CleanseNearby(context, this, Pos);
            }
        }

    }
}