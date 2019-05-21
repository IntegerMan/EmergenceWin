using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.Commands
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