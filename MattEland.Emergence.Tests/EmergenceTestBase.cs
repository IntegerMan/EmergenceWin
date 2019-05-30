using System.Collections.Generic;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Commands;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.WpfCore.ViewModels;
using MattEland.Shared.Collections;
using NUnit.Framework;

namespace MattEland.Emergence.Tests
{
    public abstract class EmergenceTestBase
    {
        public GameService GameService { get; set; }
        public GameContext Context { get; set; }
        public Player Player => Context.Player;

        public GameViewModel GameViewModel { get; set; }

        [SetUp]
        protected virtual void Initialize()
        {
            GameViewModel = new GameViewModel();
            GameService = GameViewModel.GameService;
            Context = GameViewModel.Context;
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

        [NotNull, ItemNotNull]
        protected IEnumerable<CommandSlot> SetPlayerCommands(params GameCommand[] commands)
        {
            var playerCommands = GameViewModel.Context.Player.HotbarCommands;
            playerCommands.Clear();

            commands.Each(c => playerCommands.Add(new CommandSlot(c)));

            return playerCommands;
        }

        [NotNull]
        protected CommandSlot SetPlayerCommand(GameCommand command)
        {
            var playerCommands = GameViewModel.Context.Player.HotbarCommands;
            playerCommands.Clear();

            var slot = new CommandSlot(command);
            playerCommands.Add(slot);

            return slot;
        }
    }
}