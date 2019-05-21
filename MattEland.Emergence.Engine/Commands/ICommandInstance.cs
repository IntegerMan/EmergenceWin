using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.Commands
{
    public interface ICommandInstance
    {
        [CanBeNull]
        IGameCommand Command { get; set; }
        bool IsActive { get; set; }
    }
}