namespace MattEland.Emergence.Definitions.Services
{
    public interface ISimulationManager
    {
        bool ShouldEndSimulation(ICommandContext context);
        bool AllowCorruption { get; }
    }
}