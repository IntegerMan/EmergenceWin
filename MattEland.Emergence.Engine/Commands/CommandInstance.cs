using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.Commands
{
    public class CommandInstance : ICommandInstance
    {
        public CommandInstance([CanBeNull] GameCommand command = null, bool isActive = false)
        {
            Command = command;
            IsActive = isActive;
        }

        [CanBeNull]
        public GameCommand Command { get; set; }
        public bool IsActive { get; set; }
    }
}