using System;
using System.Collections.Generic;

namespace MattEland.Emergence.Engine.Level.Generation.Prefabs
{
    public interface IPrefabService
    {
        /// <summary>
        /// Adds a prefab room to a level under construction.
        /// </summary>
        /// <param name="builder">The level builder.</param>
        /// <param name="upperLeft">The absolute position of the upper left point of the room.</param>
        /// <param name="prefab">The prefab to add</param>
        /// <param name="flipX">if set to <c>true</c> the prefab will be flipped horizontally before being applied.</param>
        /// <param name="flipY">if set to <c>true</c> the prefab will be flipped vertically before being applied.</param>
        /// <returns>A unique identifier for the prefab, used for encounter generation.</returns>
        Guid AddPrefab(ILevelBuilder builder, Pos2D upperLeft, PrefabData prefab, bool flipX = false, bool flipY = false);

        Guid AddRectangularRoom(ILevelBuilder builder, Pos2D upperLeft, Pos2D size);
        PrefabData GetPrefab(string id);

        /// <summary>
        ///     Gets a prefabricated level.
        /// </summary>
        /// <param name="levelType">Type of the level.</param>
        /// <returns>The prefabricated level.</returns>
        PrefabricatedLevel GetPrefabLevel(LevelType levelType);

        IReadOnlyCollection<PrefabData> GetPrefabs();
    }
}