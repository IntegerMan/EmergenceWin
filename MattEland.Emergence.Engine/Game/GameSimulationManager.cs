using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Game
{
    public class GameSimulationManager : ISimulationManager
    {
        public bool ShouldEndSimulation(CommandContext context)
        {
            return context.Player == null || context.Player.IsDead;
        }

        public bool AllowCorruption => true;
    }
}
