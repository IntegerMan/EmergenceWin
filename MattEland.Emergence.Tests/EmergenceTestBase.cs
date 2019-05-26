using MattEland.Emergence.Engine;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;

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

        protected GameObjectBase CreateTurret() 
            => GameObjectFactory.CreateFromObjectType("ACTOR_TURRET", GameObjectType.Actor, new Pos2D(-12, 42));
    }
}