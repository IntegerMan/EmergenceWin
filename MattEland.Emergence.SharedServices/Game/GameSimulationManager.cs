using MattEland.Emergence.Definitions.Services;

namespace MattEland.Emergence.Services.Game
{
    public class GameSimulationManager : ISimulationManager
    {
        public bool ShouldEndSimulation(ICommandContext context)
        {
            return context.Player == null || context.Player.IsDead;
        }

        public bool AllowCorruption => true;
    }
}
