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
        private static IDictionary<string, IGameCommand> _commands;

        public static IDictionary<string, IGameCommand> Commands
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

        public static IGameCommand CreateCommand([NotNull] string commandId)
        {
            if (string.IsNullOrWhiteSpace(commandId))
            {
                throw new ArgumentOutOfRangeException(nameof(commandId), "A command ID must be specified");
            }

            if (!Commands.ContainsKey(commandId.ToLowerInvariant()))
            {
                // TODO: Logging this might be a good idea
                return null;
            }

            return Commands[commandId.ToLowerInvariant()];
        }

        private static IDictionary<string, IGameCommand> BuildCommandDictionary()
        {
            var commandType = typeof(IGameCommand);

            var types = Assembly.GetExecutingAssembly().GetTypes()
                                .Where(p => commandType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract);

            var dict = new Dictionary<string, IGameCommand>();
            foreach (var type in types)
            {
                var instance = (IGameCommand)Activator.CreateInstance(type);
                dict[instance.Id.ToLowerInvariant()] = instance;
            }

            return dict;
        }

        public static ICommandInstance CreateCommandReference([CanBeNull] CommandInfoDto dto)
        {
            IGameCommand command = null;
            bool isActive = false;

            if (dto != null)
            {
                command = CreationService.CreateCommand(dto.Id);
                isActive = dto.IsActive;
            }

            return new CommandInstance(command, isActive);
        }

        public static IEnumerable<IGameCommand> RegisteredCommands => Commands.Values;
    }
}