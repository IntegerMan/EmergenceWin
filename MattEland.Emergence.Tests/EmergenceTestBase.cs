using MattEland.Emergence.Engine;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;

namespace MattEland.Emergence.Tests
{
    public abstract class EmergenceTestBase
    {
        public GameService GameService { get; set; }
        public CommandContext Context { get; set; }
        public Player Player { get; set; }

        protected GameManager GetStartedGameManager()
        {
            var manager = new GameManager();
            manager.Start();

            return manager;
        }

        protected void InitializeGameService()
        {
            GameService = new GameService();
            Context = GameService.StartNewGame();
            Player = GameService.Player;
        }

    }
}