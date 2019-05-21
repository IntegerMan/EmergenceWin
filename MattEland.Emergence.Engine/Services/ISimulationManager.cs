using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Services
{
    public interface ISimulationManager
    {
        bool ShouldEndSimulation(CommandContext context);
        bool AllowCorruption { get; }
    }
}