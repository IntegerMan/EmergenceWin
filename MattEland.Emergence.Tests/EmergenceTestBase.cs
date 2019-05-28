using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
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
            Context.ClearMessages();
        }

        [NotNull]
        protected GameService GetStartedGameManager()
        {
            var manager = BuildGameService();
            manager.StartNewGame();

            return manager;
        }

        [NotNull]
        protected GameObjectBase CreateTurret(Pos2D pos)
        {
            var turret = GameObjectFactory.CreateActor(Actors.Turret, pos);

            Context.AddObject(turret);

            return turret;
        }

        [NotNull]
        protected static GameService BuildGameService() => new GameService(new TestRandomizer(0));
    }
}