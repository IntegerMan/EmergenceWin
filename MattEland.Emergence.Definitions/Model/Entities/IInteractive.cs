using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public interface IInteractive
    {
        void Interact(ICommandContext context);
    }
}