using MattEland.Emergence.Engine;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using NUnit.Framework;

namespace MattEland.Emergence.Tests
{
    public abstract class EmergenceTestBase
    {
        public GameService GameService { get; set; }
        public CommandContext Context { get; set; }
        public Player Player => Context.Player;

        [SetUp]
        protected virtual void Initialize()
        {
            GameService = new GameService();
            Context = GameService.StartNewGame();
        }

        protected GameManager GetStartedGameManager()
        {
            var manager = new GameManager();
            manager.Start();

            return manager;
        }

        protected GameObjectBase CreateTurret(Pos2D pos)
        {
            var turret = GameObjectFactory.CreateFromObjectType("ACTOR_TURRET", GameObjectType.Actor, pos);

            Context.AddObject(turret);

            return turret;
        }
    }
}