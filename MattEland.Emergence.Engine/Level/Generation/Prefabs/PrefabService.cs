using System;
using System.Collections.Generic;
using System.Linq;

namespace MattEland.Emergence.Engine.Level.Generation.Prefabs
{
    /// <summary>
    ///     A service for interacting with prefabs and composing levels out of them.
    /// </summary>
    public sealed class PrefabService : IPrefabService
    {
        private readonly PrefabDataProvider _prefabProvider;

        /// <summary>
        ///     Constructs a new <see cref="PrefabService" />.
        /// </summary>
        public PrefabService()
        {
            _prefabProvider = new PrefabDataProvider();
        }

        /// <summary>
        /// Adds a prefab room to a level under construction.
        /// </summary>
        /// <param name="builder">The level builder.</param>
        /// <param name="upperLeft">The absolute position of the upper left point of the room.</param>
        /// <param name="prefab">The prefab to add</param>
        /// <param name="flipX">if set to <c>true</c> the prefab will be flipped horizontally before being applied.</param>
        /// <param name="flipY">if set to <c>true</c> the prefab will be flipped vertically before being applied.</param>
        /// <returns>A unique identifier for the prefab, used for encounter generation.</returns>
        public Guid AddPrefab(ILevelBuilder builder,
            Pos2D upperLeft,
            PrefabData prefab,
            bool flipX = false,
            bool flipY = false)
        {

            // Validate that the prefab was identified.
            if (prefab == null)
            {
                throw new ArgumentNullException(nameof(prefab));
            }

            // Come up with a distinct prefab ID for this particular prefab instance
            var prefabGuid = Guid.NewGuid();

            // Declare loop formulae here so they don't have to be recomputed for every cell
            Pos2D prefabDimensions = new Pos2D(prefab.Data.Max(c => c.Length), prefab.Data.Count);

            AddCellsFromPrefab(builder, upperLeft, flipX, flipY, prefabGuid, prefab, prefabDimensions);

            return prefabGuid;
        }

        /// <summary>
        /// Adds the cells from prefab to the level builder.
        /// </summary>
        /// <param name="builder">The level builder.</param>
        /// <param name="upperLeft">The upper left position of the prefab within the level.</param>
        /// <param name="flipX">if set to <c>true</c> the prefab will be flipped horizontally.</param>
        /// <param name="flipY">if set to <c>true</c> the prefab will be flipped vertically.</param>
        /// <param name="prefabGuid">The prefab instance's unique identifier.</param>
        /// <param name="prefab">The prefab.</param>
        /// <param name="prefabDimensions">The prefab dimensions.</param>
        private static void AddCellsFromPrefab(ILevelBuilder builder,
                                               Pos2D upperLeft,
                                               bool flipX,
                                               bool flipY,
                                               Guid prefabGuid,
                                               PrefabData prefab,
                                               Pos2D prefabDimensions)
        {
            // Loop over the rows in the prefab
            int y = 0;
            foreach (var row in prefab.Data)
            {
                // Loop over the cells in the prefab
                int x = 0;
                foreach (var c in row)
                {
                    // Do nothing if there's nothing assignable for the cell
                    if (c != ' ')
                    {
                        Pos2D cellPos =
                            CalculatePrefabCellPosition(new Pos2D(x, y), upperLeft, prefabDimensions, flipY, flipX);

                        var cell = builder.BuildPrefabCell(c, cellPos, prefab);

                        // Respect the IsInvulnerable flag on prefabs
                        if (prefab.IsInvulnerable)
                        {
                            foreach (var wall in cell.Objects.Where(o => o.ObjectType == GameObjectType.Wall))
                            {
                                wall.SetInvulnerable();
                            }
                        }

                        builder.AddCell(cell, prefabGuid);
                    }

                    x++;
                }

                y++;
            }
        }

        /// <summary>
        /// Calculates the prefab cell's position within the level given relative placement of the prefab and prefab manipulations.
        /// </summary>
        /// <param name="relativePos">The relative position of the cell within the prefab.</param>
        /// <param name="upperLeft">The upper left corner of the prefab placement within the game level.</param>
        /// <param name="prefabDimensions">The prefab dimensions.</param>
        /// <param name="flipY">if set to <c>true</c> the Y axis will be flipped vertically.</param>
        /// <param name="flipX">if set to <c>true</c> the X axis will be flipped horizontally.</param>
        /// <returns>Pos2D.</returns>
        public static Pos2D CalculatePrefabCellPosition(Pos2D relativePos,
            Pos2D upperLeft,
            Pos2D prefabDimensions,
            bool flipY,
            bool flipX)
        {
            // If we're flipping vertically, map to a different position
            int y;
            if (flipY)
            {
                y = prefabDimensions.Y - relativePos.Y - 1;
            }
            else
            {
                y = relativePos.Y;
            }

            // If we're flipping horizontally, map to a different slot
            int x;
            if (flipX)
            {
                x = prefabDimensions.X - relativePos.X - 1;
            }
            else
            {
                x = relativePos.X;
            }

            // Take into account the upper positions
            x += upperLeft.X;
            y += upperLeft.Y;

            return new Pos2D(x, y);
        }

        /// <summary>
        /// Builds a rectangular room of the specified dimensions.
        /// </summary>
        /// <param name="builder">The level builder.</param>
        /// <param name="upperLeft">The absolute position of the upper left point of the room.</param>
        /// <param name="size">The width and height of the room.</param>
        /// <returns>The unique identifier of the room.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// size - The X size of a constructed room must be greater than zero.
        /// or
        /// size - The Y size of a constructed room must be greater than zero.
        /// </exception>
        /// <exception cref="System.ArgumentOutOfRangeException">size - The X and Y sizes of a constructed room must each be greater than zero.</exception>
        public Guid AddRectangularRoom(ILevelBuilder builder, Pos2D upperLeft, Pos2D size)
        {
            // Validate
            if (size.X <= 0 || size.Y <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size),
                    "The X and Y sizes of a constructed room must be greater than zero.");
            }

            var roomGuid = Guid.NewGuid();

            // Loop through the bounds and add the requested room
            for (var x = upperLeft.X; x <= upperLeft.X + size.X - 1; x++)
            {
                for (var y = upperLeft.Y; y <= upperLeft.Y + size.Y - 1; y++)
                {
                    var isOnEdge = x == upperLeft.X ||
                                   x == upperLeft.X + size.X - 1 ||
                                   y == upperLeft.Y ||
                                   y == upperLeft.Y + size.Y - 1;

                    char terrain = isOnEdge ? '#' : '.';

                    // TODO: We'll want to add doors to the edges too

                    var cell = builder.BuildCell(terrain, new Pos2D(x, y));
                    builder.AddCell(cell, roomGuid);
                }
            }

            return roomGuid;
        }

        /// <summary>
        ///     Gets a prefabricated level.
        /// </summary>
        /// <param name="levelType">Type of the level.</param>
        /// <returns>The prefabricated level.</returns>
        public PrefabricatedLevel GetPrefabLevel(LevelType levelType)
        {
            return PrefabLevelProvider.GetLevel(levelType);
        }

        /// <summary>
        /// Gets the collection of available prefabs.
        /// </summary>
        /// <returns>The collection of available prefabs</returns>
        public IReadOnlyCollection<PrefabData> GetPrefabs()
        {
            var prefabs = _prefabProvider.Items;
            return prefabs.ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the prefab with the given identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The prefab or <c>null</c> if it could not be found.</returns>
        public PrefabData GetPrefab(string id)
        {
            return _prefabProvider.GetItem(id);
        }
    }
}