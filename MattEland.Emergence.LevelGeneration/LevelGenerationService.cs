using System;
using GeneticSharp.Domain.Randomizations;
using JetBrains.Annotations;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.LevelGeneration.Encounters;
using MattEland.Emergence.LevelGeneration.Prefabs;

namespace MattEland.Emergence.LevelGeneration
{

    /// <summary>
    /// A service used for generating a level.
    /// </summary>
    public sealed class LevelGenerationService
    {
        private readonly IPrefabService _prefabService;
        private readonly EncountersService _encounterService;
        private readonly IRandomization _randomization;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelGenerationService" /> class.
        /// </summary>
        /// <param name="prefabService">The prefab service.</param>
        /// <param name="encounterService">The encounter service.</param>
        public LevelGenerationService(IPrefabService prefabService, EncountersService encounterService, IRandomization randomization)
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
        /// <returns><see cref="ILevel"/> representing the structure of the generated level.</returns>
        public ILevel GenerateLevel([NotNull] LevelGenerationParameters parameters, [CanBeNull] IPlayer player)
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
                player.Position = prefabLevel.Start;
                levelBuilder.AddObject(player);
            }

            return levelBuilder.CreateLevel();
        }
    }

}