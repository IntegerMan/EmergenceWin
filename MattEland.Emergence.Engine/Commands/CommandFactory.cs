using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.Commands
{
    /// <summary>
    /// A class used to generate command objects from IDs, preventing lower layers from having to know
    /// which commands are available.
    /// </summary>
    public static class CommandFactory
    {
        [CanBeNull]
        private static IDictionary<string, GameCommand> _commands;

        [NotNull]
        public static IDictionary<string, GameCommand> Commands => _commands ?? (_commands = BuildCommandDictionary());

        [NotNull]
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

        [NotNull]
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

        public static CommandSlot CreateCommandReference([CanBeNull] GameCommand command)
        {
            var isActive = command != null && command.ActivationType == CommandActivationType.Active;

            return new CommandSlot(command, isActive);
        }

        [NotNull, ItemNotNull]
        public static IEnumerable<GameCommand> RegisteredCommands => Commands.Values;
    }
}