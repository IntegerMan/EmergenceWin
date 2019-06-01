using System;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Level.Generation.Prefabs;

namespace MattEland.Emergence.Engine.Level.Generation
{

    /// <summary>
    /// A service used for generating a level.
    /// </summary>
    public sealed class LevelGenerationService
    {
        private readonly PrefabService _prefabService;
        private readonly EncountersService _encounterService;
        private readonly IRandomization _randomization;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelGenerationService" /> class.
        /// </summary>
        /// <param name="prefabService">The prefab service.</param>
        /// <param name="encounterService">The encounter service.</param>
        public LevelGenerationService(PrefabService prefabService, EncountersService encounterService, IRandomization randomization)
        {
            _prefabService = prefabService;
            _encounterService = encounterService;
            _randomization = randomization;
        }

        /// <summary>
        /// Generates a level from the specified <paramref name="parameters"/>.
        /// </summary>
        /// <param name="parameters">The level generation parameters.</param>
        /// <param name="player">The player object.</param>
        /// <returns><see cref="LevelData"/> representing the structure of the generated level.</returns>
        public LevelData GenerateLevel([NotNull] LevelGenerationParameters parameters, [CanBeNull] Player player)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            // Create the level builder that will manage the assembly process
            var levelBuilder = new LevelBuilder(_prefabService, _encounterService, _randomization);

            // Copy over the prefabs defined in the instruction set
            var prefabLevel = _prefabService.GetPrefabLevel(parameters.LevelType);
            levelBuilder.LevelName = prefabLevel.Name;
            levelBuilder.LevelId = parameters.LevelType;

            foreach (var instr in prefabLevel.Instructions)
            {
                levelBuilder.AddInstruction(instr);
            }

            // Ensure we start where the level says we should
            levelBuilder.PlayerStart = prefabLevel.Start;

            // Ordinarily the player won't be null, but in some debug / visualization settings, it can happen
            if (player != null)
            {
                player.Pos = prefabLevel.Start;
                levelBuilder.AddObject(player);
            }

            return levelBuilder.CreateLevel();
        }
    }

}