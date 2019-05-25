using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Commands
{
    /// <summary>
    /// A class used to generate command objects from IDs, preventing lower layers from having to know
    /// which commands are available.
    /// </summary>
    public static class CommandFactory
    {
        private static IDictionary<string, GameCommand> _commands;

        public static IDictionary<string, GameCommand> Commands
        {
            get
            {
                // Lazy load the dictionary
                if (_commands == null)
                {
                    _commands = BuildCommandDictionary();
                }

                return _commands;
            }
        }

        public static GameCommand CreateCommand([NotNull] string commandId)
        {
            if (string.IsNullOrWhiteSpace(commandId))
            {
                throw new ArgumentOutOfRangeException(nameof(commandId), "A command ID must be specified");
            }

            if (!Commands.ContainsKey(commandId.ToLowerInvariant()))
            {
                throw new NotSupportedException($"{commandId} is not a supported command");
            }

            return Commands[commandId.ToLowerInvariant()];
        }

        private static IDictionary<string, GameCommand> BuildCommandDictionary()
        {
            var commandType = typeof(GameCommand);

            var types = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(p => commandType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            var dict = new Dictionary<string, GameCommand>();
            foreach (var type in types)
            {
                var instance = (GameCommand)Activator.CreateInstance(type);
                dict[instance.Id.ToLowerInvariant()] = instance;
            }

            return dict;
        }

        public static ICommandInstance CreateCommandReference([CanBeNull] CommandInfoDto dto)
        {
            GameCommand command = null;
            bool isActive = false;

            if (dto != null)
            {
                command = CreationService.CreateCommand(dto.Id);
                isActive = dto.IsActive;
            }

            return new CommandInstance(command, isActive);
        }

        public static IEnumerable<GameCommand> RegisteredCommands => Commands.Values;
    }
}