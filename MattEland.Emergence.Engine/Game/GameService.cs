﻿using System;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Level.Generation;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Level.Generation.Prefabs;
using MattEland.Emergence.Engine.Loot;
using MattEland.Emergence.Engine.Model;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Game
{
    /// <summary>
    /// A service for dealing with high-level game operations such as new games or player moves.
    /// </summary>
    public sealed class GameService
    {
        [NotNull] private readonly LevelGenerationService _levelService;
        [NotNull] private readonly LootProvider _lootProvider;
        [NotNull] private readonly CombatManager _combatManager;
        [NotNull] private readonly EntityDefinitionService _entityProvider;

        private LevelData _level;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        public GameService()
        {
            _entityProvider = new EntityDefinitionService();
            _combatManager = new CombatManager();
            _lootProvider = new LootProvider();
            _levelService = new LevelGenerationService(new PrefabService(), new EncountersService(), new BasicRandomization());

            GameCreationConfigurator.ConfigureObjectCreation();
        }

        public CommandContext StartNewGame([CanBeNull] NewGameParameters parameters = null)
        {
#if DEBUG
            const string defaultPlayerId = "ACTOR_PLAYER_DEBUGGER";
#else
            const string defaultPlayerId = "ACTOR_PLAYER_ANTIVIRUS";
#endif

            if (parameters == null)
            {
                parameters = new NewGameParameters
                {
                    CharacterId = defaultPlayerId
                };
            } else if (string.IsNullOrWhiteSpace(parameters.CharacterId))
            {
                parameters.CharacterId = defaultPlayerId;
            }

            // Set up the basic parameters
            var levelParameters = new LevelGenerationParameters { LevelType = LevelType.Tutorial };
            Player = CreationService.CreatePlayer(parameters.CharacterId);
            _level = _levelService.GenerateLevel(levelParameters, Player);

            // Ensure line of sight is calculated
            var context = new CommandContext(_level, this, _entityProvider, _combatManager, _lootProvider);
            context.CalculateLineOfSight(Player);

            _level.Objects.Each(o => context.CreatedObject(o));

            return context;
        }

        public CommandContext HandleGameMove(MoveDirection direction)
        {
            NumMoves++;

            var context = new CommandContext(_level, this, _entityProvider, _combatManager, _lootProvider);

            // Give objects and actors a chance to react to the current game state
            foreach (var obj in context.Level.Objects.ToList())
            {
                obj.ApplyActiveEffects(context);
            }

            // Interact with all objects in the tile
            var targetPos = Player.Pos.GetNeighbor(direction);
            foreach (var obj in context.Level.Objects.Where(o => o.Pos == targetPos).OrderByDescending(o => o.ZIndex))
            {
                if (obj.OnActorAttemptedEnter(context, Player))
                {
                    break;
                }
            }

            // Give objects and actors a chance to react to the changed state
            foreach (var obj in context.Level.Objects.ToList())
            {
                obj.MaintainActiveEffects(context);
            }

            // Ensure that vision is accurate for the player before sending back to the client
            context.CalculateLineOfSight(context.Player);

            // The player can change so make sure we keep a reference to the correct player object
            Player = context.Player;

            return context;
        }

        public int NumMoves { get; set; }
        public Player Player { get; private set; }

        public void MoveToLevel(LevelType nextLevelType, CommandContext commandContext)
        {
            var levelParams = new LevelGenerationParameters
            {
                LevelType = nextLevelType,
                PlayerId = commandContext.Player.ObjectId
            };

            commandContext.Player.ClearKnownCells();

            var nextLevel = _levelService.GenerateLevel(levelParams, commandContext.Player);
            commandContext.SetLevel(nextLevel);
        }

    }
}