using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Commands;

namespace MattEland.Emergence.Commands
{
    public class CommandInstance : ICommandInstance
    {
        public CommandInstance([CanBeNull] IGameCommand command = null, bool isActive = false)
        {
            Command = command;
            IsActive = isActive;
        }

        [CanBeNull]
        public IGameCommand Command { get; set; }
        public bool IsActive { get; set; }
    }
}