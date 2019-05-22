using System;
using System.Collections.Generic;
using System.Linq;
using GeneticSharp.Domain.Randomizations;
using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Level.Generation.Encounters;
using MattEland.Emergence.Engine.Level.Generation.Prefabs;
using MattEland.Emergence.Engine.Services;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Level.Generation
{
    /// <summary>
    /// A utility class used for constructing and manipulating LevelData instances.
    /// </summary>
    public class LevelBuilder
    {
        private readonly PrefabService _prefabService;
        private readonly EncountersService _encountersService;
        private readonly IRandomization _randomization;
        private readonly IDictionary<Pos2D, GameCell> _cells;
        private readonly IDictionary<Pos2D, Guid> _cellRoomId;
        private readonly IDictionary<char, GameObjectType> _terrainObjects;
        private readonly IDictionary<Guid, PrefabData> _roomPrefabs;
        private readonly IDictionary<Guid, LevelAssemblyInstruction> _roomInstructions;

        /// <summary>
        /// Initializes a new instance of the <see cref="LevelBuilder" /> class.
        /// </summary>
        /// <param name="prefabService">The prefab service.</param>
        /// <param name="encounterService">The encounter service.</param>
        /// <param name="randomization">The randomization</param>
        public LevelBuilder(
            PrefabService prefabService,
            EncountersService encounterService,
            IRandomization randomization)
        {
            PlayerStart = new Pos2D(0, 0);

            _prefabService = prefabService;
            _encountersService = encounterService;
            _randomization = randomization;
            _cells = new Dictionary<Pos2D, GameCell>();
            _cellRoomId = new Dictionary<Pos2D, Guid>();
            _roomPrefabs = new Dictionary<Guid, PrefabData>();
            _roomInstructions = new Dictionary<Guid, LevelAssemblyInstruction>();
            _terrainObjects = new Dictionary<char, GameObjectType>();

            PopulateObjectDictionary();
        }

        /// <summary>
        /// Populates the object dictionary with translations of cell terrain characters to their corresponding object codes.
        /// </summary>
        private void PopulateObjectDictionary()
        {
            _terrainObjects.Add('#', GameObjectType.Wall);
            _terrainObjects.Add('t', GameObjectType.Treasure);
            _terrainObjects.Add('+', GameObjectType.Door);
            _terrainObjects.Add('$', GameObjectType.CommandPickup);
            _terrainObjects.Add('C', GameObjectType.Core);
            _terrainObjects.Add('X', GameObjectType.Divider);
            _terrainObjects.Add(',', GameObjectType.Cabling);
            _terrainObjects.Add('T', GameObjectType.Turret);
            _terrainObjects.Add('|', GameObjectType.Firewall);
            _terrainObjects.Add('?', GameObjectType.Help);
            _terrainObjects.Add('>', GameObjectType.Exit);
            _terrainObjects.Add('<', GameObjectType.Entrance);
            _terrainObjects.Add('*', GameObjectType.Service);
            _terrainObjects.Add('d', GameObjectType.DataStore);
            _terrainObjects.Add('~', GameObjectType.Water);
        }

        /// <summary>
        /// Gets the cells in the level thus far. This is a read-only collection.
        /// </summary>
        /// <value>The cells.</value>
        public IReadOnlyCollection<GameCell> Cells => _cells.Values.ToList();

        /// <summary>
        /// Gets or sets the player's starting position.
        /// </summary>
        /// <value>The player's starting position.</value>
        public Pos2D PlayerStart { get; set; }

        /// <summary>
        /// Gets or sets the level identifier.
        /// </summary>
        /// <value>The level identifier.</value>
        public LevelType LevelId { get; set; }

        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        /// <value>The name of the level.</value>
        public string LevelName { get; set; } = "Untitled Level";

        /// <summary>
        /// Adds a <paramref name="cell"/> to the level, potentially merging it with another cell at that position.
        /// </summary>
        /// <param name="cell">The cell to add.</param>
        /// <param name="roomId">The unique identifier of the room.</param>
        public void AddCell(GameCell cell, Guid roomId)
        {
            if (_cells.ContainsKey(cell.Pos))
            {
                _cells[cell.Pos] = MergeCells(_cells[cell.Pos], cell);
            }
            else
            {
                _cells[cell.Pos] = cell;
            }

            _cellRoomId[cell.Pos] = roomId;
        }

        /// <summary>
        /// Merges two cells together to form a single combined cell.
        /// </summary>
        /// <param name="oldCell">The cell already present.</param>
        /// <param name="newCell">The cell to merge into the original cell.</param>
        /// <returns>GameCell.</returns>
        private static GameCell MergeCells(GameCell oldCell, GameCell newCell)
        {
            // Make sure invulnerable flags don't get lost when merging cells
            if (oldCell.Objects.Any(o => o.ObjectType == GameObjectType.Wall && o.IsInvulnerable))
            {
                foreach (var obj in newCell.Objects.Where(o => o.ObjectType == GameObjectType.Wall))
                {
                    obj.SetInvulnerable();
                }
            }

            return newCell;
        }


        /// <summary>
        /// Adds a level assembly instruction to the game world.
        /// </summary>
        /// <param name="instruction">The instruction.</param>
        /// <returns>Guid.</returns>
        public Guid AddInstruction(LevelAssemblyInstruction instruction)
        {
            var roomId = AddPrefab(new Pos2D(instruction.X, instruction.Y), instruction.PrefabId, instruction.FlipX, instruction.FlipY);

            _roomInstructions[roomId] = instruction;

            return roomId;
        }

        /// <summary>
        /// Adds a prefab to the level at the specified position.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <param name="prefabId">The prefab identifier.</param>
        /// <param name="flipX">if set to <c>true</c> the x axis will be flipped.</param>
        /// <param name="flipY">if set to <c>true</c> the y axis will be flipped.</param>
        /// <returns>The unique identifier of the prefab instance, for encounter generation.</returns>
        private Guid AddPrefab(Pos2D pos, string prefabId, bool flipX = false, bool flipY = false)
        {
            // Go find the thing
            var prefab = _prefabService.GetPrefab(prefabId);

            if (prefab == null)
            {
                throw new InvalidOperationException($"Could not locate prefab {prefabId} for level generation");
            }

            var roomId = _prefabService.AddPrefab(this, pos, prefab, flipX, flipY);

            _roomPrefabs[roomId] = prefab;

            return roomId;
        }

        /// <summary>
        /// Adds a rectangular room to the level.
        /// </summary>
        /// <param name="upperLeft">The upper left corner for the room.</param>
        /// <param name="size">The size of the room.</param>
        /// <returns>The unique identifier of the room, for encounter generation.</returns>
        public Guid AddRectangularRoom(Pos2D upperLeft, Pos2D size)
        {
            return _prefabService.AddRectangularRoom(this, upperLeft, size);
        }

        /// <summary>
        /// Creates a level out of the current state and returns that <see cref="LevelData"/> instance.
        /// </summary>
        /// <returns>LevelData.</returns>
        /// <exception cref="InvalidOperationException">Cannot finalize a level if no cells have been added</exception>
        public LevelData CreateLevel()
        {

            if (!Cells.Any())
            {
                throw new InvalidOperationException("Cannot finalize a level if no cells have been added");
            }

            // Build out the level object
            var level = CreationService.CreateLevel(LevelId, LevelName, PlayerStart);

            // Copy over the cells into the level
            foreach (var cell in Cells)
            {
                level.AddCell(cell);
            }

            // Set the coordinates of the map based on the cells present
            level.UpperLeft = new Pos2D(level.Cells.Min(c => c.Pos.X), level.Cells.Min(c => c.Pos.Y));
            level.LowerRight = new Pos2D(level.Cells.Max(c => c.Pos.X), level.Cells.Max(c => c.Pos.Y));

            // Mark command requires that the mark pos default to the start location
            level.MarkedPos = PlayerStart;

            // Ensure all walls are marked external that should be
            FinalizePlacedWalls(level);

            // Loop over all door cells and ensure that they have floors without walls on the other side of them.
            FinalizeDoors(level);

            FinalizeFloors(level);

            GenerateActors(level);

            return level;
        }

        private static void FinalizeFloors(LevelData level)
        {
            foreach (var cell in level.Cells.Where(c => c.FloorType != FloorType.Void && !c.HasNonActorObstacle))
            {
                level.AddObject(new Floor(new GameObjectDto
                {
                    Pos = cell.Pos.SerializedValue
                }, cell.FloorType));
            }
        }

        private static void FinalizePlacedWalls(LevelData level) => 
            level.Objects.Where(o => o.ObjectType == GameObjectType.Wall && level.IsPosExterior(o.Pos)).Each(w => w.State = "External");

        /// <summary>
        /// Generates actors for the level and places their contents inside of the level.
        /// </summary>
        /// <param name="level">The level.</param>
        private void GenerateActors(LevelData level)
        {
            // Group all cells by room Ids
            var roomCells = BuildRoomMappingDictionary(level);

            // Add objects to the encounter
            foreach (var roomKvp in roomCells)
            {
                var cells = roomKvp.Value;

                // Grab the prefab info for the room
                PrefabData prefab = _roomPrefabs.ContainsKey(roomKvp.Key) ? _roomPrefabs[roomKvp.Key] : null;

                // If we need to add cores, do so now
                if (prefab != null && prefab.MaxCores > 0)
                {
                    AddCoresToPrefab(cells, prefab);
                }

                // Grab the encounter set identifier
                string encounterSetId = null;
                if (_roomInstructions.ContainsKey(roomKvp.Key)) // TODO: Rectangle rooms will never have a key in here
                {
                    var instruction = _roomInstructions[roomKvp.Key];

                    encounterSetId = instruction?.EncounterSet;
                }

                _encountersService.GenerateEncounterFromEncounterSet(cells, encounterSetId, _randomization);

            }
        }

        private void AddCoresToPrefab(IEnumerable<GameCell> cells, PrefabData prefab)
        {
            int numCores = _randomization.GetInt(prefab.MinCores, prefab.MaxCores);
            if (numCores > 0)
            {
                var elements = new List<EncounterElement>(numCores);
                for (int i = 0; i < numCores; i++)
                {
                    elements.Add(new EncounterElement { ObjectType = GameObjectType.Core, ObjectId = "ACTOR_CORE" });
                }


                EncountersService.AddEncounterElementsToCells(elements, cells, _randomization);
            }
        }

        private Dictionary<Guid, ICollection<GameCell>> BuildRoomMappingDictionary(LevelData level)
        {
            var roomCells = new Dictionary<Guid, ICollection<GameCell>>();
            foreach (KeyValuePair<Pos2D, GameCell> posKvp in _cells)
            {
                Guid roomId = _cellRoomId[posKvp.Key];

                if (!roomCells.ContainsKey(roomId))
                {
                    roomCells[roomId] = new List<GameCell>();
                }

                var cell = level.GetCell(posKvp.Key);
                if (cell != null)
                {
                    roomCells[roomId].Add(cell);
                }
            }

            return roomCells;
        }

        /// <summary>
        /// Finalizes the doors on the level by removing door indicators that don't connect to anything meaningful.
        /// </summary>
        /// <param name="level">The level.</param>
        private static void FinalizeDoors(LevelData level)
        {
            bool Finder(GameObjectBase c) => c.ObjectType == GameObjectType.Door;

            var doorCells = level.Cells.Where(c => c.Objects.Any(Finder));
            foreach (var cell in doorCells.OrderBy(c => c.Pos.Y).ThenBy(c => c.Pos.X))
            {
                // If it's supposed to have a door, just roll with it.
                if (DetermineIfCellShouldHaveDoor(level, cell))
                {
                    continue;
                }

                // Remove all doors
                cell.RemoveAllObjects(Finder);

                // Replace the door placeholder with a wall
                cell.AddObject(CreationService.CreateWall(cell.Pos, level.IsPosExterior(cell.Pos)));

                if (cell.FloorType == FloorType.CautionMarker)
                {
                    cell.FloorType = FloorType.Normal;
                }
            }
        }

        /// <summary>
        /// Determines if the given <paramref name="cell"/> should have a door.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="cell">The cell.</param>
        /// <returns><c>true</c> if the cell should have a door, <c>false</c> otherwise.</returns>
        private static bool DetermineIfCellShouldHaveDoor(LevelData level, GameCell cell)
        {
            // NOTE: Potential performance optimization here might be to give a query to the level to get these four cells at once
            var west = level.GetCell(cell.Pos.Add(-1, 0));
            var east = level.GetCell(cell.Pos.Add(1, 0));
            var north = level.GetCell(cell.Pos.Add(0, -1));
            var south = level.GetCell(cell.Pos.Add(0, 1));

            return (IsOpenCell(west) && IsOpenCell(east)) || (IsOpenCell(north) && IsOpenCell(south));
        }

        private static bool IsOpenCell(GameCell GameCell) => GameCell != null && GameCell.Objects.All(c => c.ObjectType != GameObjectType.Wall);

        /// <summary>
        /// Gets the game object a given character on a terrain map
        /// </summary>
        /// <param name="terrain">The character representing the cell's terrain.</param>
        /// <param name="pos">The position of the cell. This is used for instantiating the object at the correct position.</param>
        /// <returns>The Game Object.</returns>
        public GameObjectBase GetGameObjectFromCellTerrain(char terrain, Pos2D pos)
        {
            if (!_terrainObjects.ContainsKey(terrain))
            {
                return null;
            }

            var objType = _terrainObjects[terrain];
            return objType == GameObjectType.Wall 
                ? CreationService.CreateWall(pos, false) 
                : CreationService.CreateObject(null, objType, pos);
        }

        /// <summary>
        /// Gets the floor type from a given terrain character.
        /// </summary>
        /// <param name="terrain">The terrain character representation.</param>
        /// <returns>The type of floor to render at that location.</returns>
        public static FloorType GetFloorTypeFromTerrain(char terrain)
        {
            switch (terrain)
            {
                default:
                    return FloorType.Normal;

                case '=':
                case '+':
                    return FloorType.CautionMarker;

                case '_':
                    return FloorType.Walkway;

                case '\'':
                    return FloorType.DecorativeTile;
            }
        }

        /// <summary>
        /// Builds out a <see cref="GameCell" /> for the given parameters and returns that object.
        /// </summary>
        /// <param name="terrain">The type of terrain at that cell.</param>
        /// <param name="point">The absolute location of the cell within the level.</param>
        /// <returns>The constructed <see cref="GameCell" /> instance.</returns>
        public GameCell BuildCell(char terrain, Pos2D point)
        {
            var cell = new GameCell
            {
                FloorType = GetFloorTypeFromTerrain(terrain),
                Pos = point
            };

            var obj = GetGameObjectFromCellTerrain(terrain, point);
            if (obj != null)
            {
                cell.AddObject(obj);
            }

            return cell;
        }


        public GameCell BuildPrefabCell(char terrain, Pos2D point, PrefabData sourcePrefab)
        {
            var mapping = sourcePrefab.Mapping?.FirstOrDefault(m => m.Char == terrain);

            if (mapping == null)
            {
                return BuildCell(terrain, point);
            }

            var cell = new GameCell
            {
                FloorType = FloorType.Normal, // TODO: This may need to go on the mapping as well
                Pos = point
            };

            // This makes debugging a bit easier
            if (terrain == '<' || terrain == '>')
            {
                cell.FloorType = FloorType.DecorativeTile;
            }

            var obj = CreationService.CreateObject(mapping.ObjId, mapping.ObjType, point);
            cell.AddObject(obj);

            return cell;
        }

        public void AddObject(GameObjectBase gameObject)
        {
            if (!_cells.ContainsKey(gameObject.Pos))
            {
                _cells[gameObject.Pos] = new GameCell();
            }

            var cell = _cells[gameObject.Pos];
            cell.AddObject(gameObject);
        }
    }
}