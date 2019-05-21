using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Engine.Services
{
    public class AlwaysOnSimulationManager : ISimulationManager
    {
        public bool ShouldEndSimulation(CommandContext context)
        {
            return false;
        }

        public bool AllowCorruption => false;
    }
}