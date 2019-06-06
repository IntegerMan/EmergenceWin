using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Effects;
using MattEland.Emergence.Engine.Entities.Items;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Player : Actor
    {
        public Player(Pos2D pos, ActorType playerType) : base(pos)
        {
            Team = Alignment.Player;
            PlayerType = playerType;

            HotbarCommands = new List<CommandSlot>();
            // TODO: CreateCommandReferences(dto.Hotbar, HotbarCommands);

            StoredCommands = new List<CommandSlot>();
            // TODO: CreateCommandReferences(dto.StoredCommands, StoredCommands);
        }

        /*
        private static void CreateCommandReferences([CanBeNull] IEnumerable<CommandInfoDto> commandDtos,
            [NotNull] ICollection<CommandSlot> commandRefCollection)
        {
            if (commandDtos == null) return;

            foreach (var commandInfoDto in commandDtos)
            {
                CommandSlot reference = null;

                if (commandInfoDto != null)
                {
                    reference = CommandFactory.CreateCommandReference(commandInfoDto);
                }

                commandRefCollection.Add(reference);
            }
        }
        */

        [ItemCanBeNull] public IList<CommandSlot> HotbarCommands { get; }

        [ItemCanBeNull] public IList<CommandSlot> StoredCommands { get; }

        [ItemNotNull]
        public IEnumerable<CommandSlot> Commands
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

        public bool AttemptPickupItem(GameContext context, GameObjectBase item)
        {
            // TODO: this won't work once we have more items that can be picked up
            CommandPickup commandPickup = (CommandPickup) item;
            var command = CommandFactory.CreateCommand(commandPickup.CommandId);

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

            int index = HotbarCommands.IndexOf(null);
            if (index >= 0)
            {
                HotbarCommands[index] = CommandFactory.CreateCommandReference(command);
                context.AddMessage($"{Name} picks up {command.Name}", ClientMessageType.Success);
                return true;
            }

            index = StoredCommands.IndexOf(null);
            if (index >= 0)
            {
                StoredCommands[index] = CommandFactory.CreateCommandReference(command);
                context.AddMessage($"{Name} picks up {command.Name} and stores it", ClientMessageType.Success);
                return true;
            }

            context.AddMessage($"{Name} does not have enough free space to pick up {command.Name}", ClientMessageType.Failure);
            return false;
        }

        public override GameObjectType ObjectType => GameObjectType.Player;

        public override string Name => "Player"; // TODO switch on player type

        /// <inheritdoc />
        public override bool IsPlayer => true;

        public override char AsciiChar => '@';

        public override int Strength { get; } // TODO switch on player type
        public override int Defense { get; } // TODO switch on player type
        public override int Accuracy { get; } // TODO switch on player type
        public override int Evasion { get; } // TODO switch on player type
        public override decimal LineOfSightRadius => 5.25M; // TODO: Something more flexible
        public ActorType PlayerType { get; }

        /// <inheritdoc />
        public override bool IsCommandActive(GameCommand command) => 
            HotbarCommands.Any(c => c != null && c.IsActive && c.Command == command);

        public override void SetCommandActiveState(GameCommand command, bool isActive)
        {
            base.SetCommandActiveState(command, isActive);

            if (command == null)
            {
                return;
            }

            var match = HotbarCommands.FirstOrDefault(c => c?.Command?.Id == command.Id);

            if (match != null && command?.Id != null)
            {
                match.IsActive = isActive;
            }
        }

        public override void MaintainActiveEffects(GameContext context)
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
                        context.AddMessage($"{cmd.Command.Name} deactivates due to lack of available operations", ClientMessageType.Failure);
                        context.AddEffect(new DeactivatedEffect(this, cmd.Command.Name));
                    }

                    cmd.IsActive = false;
                    continue;
                }

                AdjustOperationsPoints(-cmd.Command.MaintenanceCost);

                cmd.Command.ApplyEffect(context, this, Pos);
            }

            // Death should end the game
            if (IsDead && !context.IsGameOver)
            {
                context.IsGameOver = true;
            }
        }

    }
}