using System;
using System.Collections.Generic;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Level.Generation;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Level.Generation.Prefabs;
using MattEland.Emergence.Engine.Loot;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Model.EngineDefinitions;
using MattEland.Emergence.Engine.Model.Messages;

namespace MattEland.Emergence.Engine
{
    public class GameManager
    {
        public GameManager()
        {
            _service = CreateService();
        }

        [NotNull]
        private static GameService CreateService()
        {
            var entityService = new EntityDefinitionService();
            var combatManager = new CombatManager();
            var loot = new LootProvider();
            var randomization = new BasicRandomization();
            var levelBuilder = new LevelGenerationService(new PrefabService(), new EncountersService(), randomization);

            return new GameService(levelBuilder, combatManager, loot, entityService);
        }

        [NotNull]
        private readonly GameService _service;

        public GameStatus State { get; private set; }

        [CanBeNull]
        public Player Player { get; }

        public IEnumerable<GameMessage> Start()
        {
            // TODO: Should this be static and create a new instance of a GameManager instead?

            if (State != GameStatus.NotStarted) throw new InvalidOperationException("The game has already been started");

            var context = _service.StartNewGame(new NewGameParameters()
            {
                CharacterId = null
            });

            State = GameStatus.Ready;

            return context.Messages;
        }

        public IEnumerable<GameMessage> MovePlayer(MoveDirection direction)
        {
            if (State != GameStatus.Ready) throw new InvalidOperationException("The game is not ready for input");

            State = GameStatus.Executing;

            var context = _service.HandleGameMove(direction);

            State = GameStatus.Ready;

            return context.Messages;
        }
    }


}
