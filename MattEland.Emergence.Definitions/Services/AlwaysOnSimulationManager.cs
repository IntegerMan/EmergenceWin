namespace MattEland.Emergence.Definitions.Services
{
    public class AlwaysOnSimulationManager : ISimulationManager
    {
        public bool ShouldEndSimulation(ICommandContext context)
        {
            return false;
        }

        public bool AllowCorruption => false;
    }
}