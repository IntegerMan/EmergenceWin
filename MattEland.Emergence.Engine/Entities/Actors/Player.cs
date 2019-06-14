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
        public Player(Pos2D pos, PlayerType playerType) : base(pos)
        {
            PlayerType = playerType;

            HotbarCommands = new List<CommandSlot>();
            StoredCommands = new List<CommandSlot>();
        }

        public override int Strength
        {
            get
            {
                switch (PlayerType)
                {
                    case PlayerType.Debugger: return 25;
                    case PlayerType.Game: return 3;
                    case PlayerType.AntiVirus: return 3;
                    default: return 2;
                }
            }
        }

        public override int Defense
        {
            get
            {
                switch (PlayerType)
                {
                    case PlayerType.Debugger: return 5;
                    default: return 1;
                }
            }
        }

        public override int Accuracy
        {
            get
            {
                switch (PlayerType)
                {
                    case PlayerType.Debugger: return 100;
                    case PlayerType.AntiVirus: return 95;
                    default: return 90;
                }
            }
        }

        public override int Evasion
        {
            get
            {
                switch (PlayerType)
                {
                    case PlayerType.AntiVirus: return 15;
                    default: return 20;
                }
            }
        }

        public override decimal LineOfSightRadius
        {
            get
            {
                switch (PlayerType)
                {
                    case PlayerType.Search: return 5.75M;
                    case PlayerType.AntiVirus: return 4.5M;
                    case PlayerType.Game: return 4.75M;
                    default: return 5.25M;    
                }
            }
        }

        public override bool BlocksSight => false;

        protected override void InitializeProtected()
        {
            base.InitializeProtected();
            Team = Alignment.Player;

            if (PlayerType == PlayerType.Debugger)
            {
                MaxOperations = 50;
                MaxStability = 50;
            }
            else
            {
                MaxStability = 10;
                MaxOperations = 10;
            }
        }

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
            if (commandPickup.CommandId == null)
            {
                context.AddError($"{Name} attempts to pick up the command but it vanishes into the void");
                return true;
            }
            
            var command = CommandFactory.CreateCommand(commandPickup.CommandId);

            if (item.IsCorrupted)
            {
                context.AddMessage($"{Name} picks up the corrupted item and is struck by a virus!", ClientMessageType.Failure);
                var message = context.CombatManager.HurtObject(context, item, this, 3, "infects", DamageType.Combination);
                context.AddMessage(message, ClientMessageType.Failure);
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

        public override string Name
        {
            get
            {
                switch (PlayerType)
                {
                    case PlayerType.Logistics: return "Logistics AI";
                    case PlayerType.Forecast: return "Weather Forecast AI";
                    case PlayerType.Game: return "Game AI";
                    case PlayerType.Search: return "Search AI";
                    case PlayerType.Malware: return "Malware AI";
                    case PlayerType.Debugger: return "Debugging Assistant";
                    case PlayerType.AntiVirus: return "Anti-Virus AI";
                    default: return "Player";
                }
            }
        }

        /// <inheritdoc />
        public override bool IsPlayer => true;

        public override char AsciiChar => '@';

        public PlayerType PlayerType { get; }

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
        }

    }
}