using System;
using System.Collections.Generic;
using System.Linq;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Level
{
    /// <summary>
    /// Contains information on a game level's structure
    /// </summary>
    public class LevelData
    {
        private IDictionary<Pos2D, GameCell> _cells;
        private readonly IDictionary<Pos2D, bool> _sightBlockerCache = new Dictionary<Pos2D, bool>();

        /// <summary>
        /// Gets or sets the level identifier.
        /// </summary>
        /// <value>The level identifier.</value>
        public LevelType Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the level.
        /// </summary>
        /// <value>The name of the level.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the upper left corner of the level. This can potentially include negative X / Y's.
        /// </summary>
        /// <value>The upper left corner.</value>
        public Pos2D UpperLeft { get; set; }

        /// <summary>
        /// Gets or sets the lower right corner of the level. This can potentially include negative X / Y's
        /// </summary>
        /// <value>The lower right corner.</value>
        public Pos2D LowerRight { get; set; }

        /// <summary>
        /// Gets or sets the collection of cells associated with the level.
        /// </summary>
        /// <value>The cells.</value>
        public ICollection<GameCell> Cells { get; set; } = new List<GameCell>();

        /// <summary>
        /// Gets or sets the player's start position.
        /// </summary>
        /// <value>The player's start position.</value>
        public Pos2D PlayerStart { get; set; }

        /// <summary>
        /// Gets the cell at the specified position, or returns null if no cell was found.
        /// </summary>
        /// <param name="pos">The position.</param>
        /// <returns>The cell.</returns>
        public GameCell GetCell(Pos2D pos) => !CellsDictionary.ContainsKey(pos) ? null : _cells[pos];

        private IDictionary<Pos2D, GameCell> CellsDictionary
        {
            get
            {
                if (_cells == null)
                {
                    _cells = new Dictionary<Pos2D, GameCell>(Cells.Count);
                    foreach (var cell in Cells)
                    {
                        _cells[cell.Pos] = cell;
                    }
                }

                return _cells;
            }
        }

        public IEnumerable<Actor> Cores => _cells.Values.Where(c => c.Core != null).Select(c => c.Core);
        public IEnumerable<Actor> Actors => _cells.Values.Where(c => c.Actor != null).Select(c => c.Actor);
        public IEnumerable<GameObjectBase> Objects => _cells.Values.SelectMany(c => c.Objects);

        public bool HasAdminAccess { get; set; }

        public Pos2D MarkedPos { get; set; }

        /// <summary>
        /// Adds a cell to the collection of cells.
        /// </summary>
        /// <param name="cell">The cell.</param>
        public void AddCell(GameCell cell)
        {
            CellsDictionary[cell.Pos] = cell;
            Cells.Add(cell);
        }

        public void MoveObject(GameObjectBase obj, Pos2D newPos)
        {
            var currentCell = GetCell(obj.Pos);
            currentCell?.RemoveObject(obj);

            var newCell = GetCell(newPos);
            newCell.AddObject(obj);

            ClearVisibilityCache();
        }

        /// <summary>
        /// Removes any instance of <paramref name="obj"/> from the level.
        /// </summary>
        /// <param name="obj">The object to remove.</param>
        public void RemoveObject(GameObjectBase obj)
        {
            foreach (var cell in _cells.Values)
            {
                cell.RemoveObject(obj);
            }

            ClearVisibilityCache();
        }

        public void ClearVisibilityCache() => _sightBlockerCache.Clear();

        public bool IsPosExterior(Pos2D pos) => pos.X == UpperLeft.X || pos.X == LowerRight.X || pos.Y == LowerRight.Y || pos.Y == UpperLeft.Y;

        public IEnumerable<GameCell> GetAdjacentCells(Pos2D pos)
        {
            foreach (var dir in new[] { MoveDirection.Up, MoveDirection.Right, MoveDirection.Down, MoveDirection.Left })
            {
                var cell = GetCell(pos.GetNeighbor(dir));
                if (cell != null)
                {
                    yield return cell;
                }
            }
        }

        public IEnumerable<GameCell> GetCellAndAdjacent(Pos2D pos)
        {
            var cell = GetCell(pos);
            if (cell != null)
            {
                yield return cell;
            }

            foreach (var adjacentCell in GetAdjacentCells(pos))
            {
                yield return adjacentCell;
            }
        }

        public IEnumerable<GameCell> GetCellsInSquare(Pos2D pos, int radius)
        {
            int minX = pos.X - radius;
            int maxX = pos.X + radius;
            int minY = pos.Y - radius;
            int maxY = pos.Y + radius;

            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    var cell = GetCell(new Pos2D(x, y));

                    if (cell != null)
                    {
                        yield return cell;
                    }
                }
            }
        }

        public bool HasSightBlocker(Pos2D pos)
        {
            // Attempt to grab from cache
            if (_sightBlockerCache.ContainsKey(pos))
            {
                return _sightBlockerCache[pos];
            }

            // Calculate whether or not things have a sight blocker
            var cell = GetCell(pos);
            var hasSightBlocker = cell != null && cell.BlocksSight;

            // Cache it for next time
            _sightBlockerCache[pos] = hasSightBlocker;

            return hasSightBlocker;
        }

        public IEnumerable<GameObjectBase> GetTargetsAtPos(Pos2D pos)
        {
            var cell = GetCell(pos);

            if (cell == null)
            {
                yield break;
            }

            foreach (var obj in cell.Objects.Where(o => o.IsTargetable))
            {
                yield return obj;
            }
        }

        /// <summary>
        /// Adds a game object to the level.
        /// </summary>
        /// <param name="gameObject">The game object.</param>
        /// <exception cref="InvalidOperationException">Thrown if the specified object's position does not correspond with a known cell</exception>
        public void AddObject(GameObjectBase gameObject)
        {
            var cell = GetCell(gameObject.Pos);
            if (cell == null)  throw new InvalidOperationException($"Could not find a cell at {gameObject.Pos.ToString()} to add an object to");

            cell.AddObject(gameObject);
        }

        /// <summary>
        /// Finds the player and returns it.
        /// </summary>
        /// <returns>The player object.</returns>
        public Player FindPlayer() => (Player)Cells.Where(c => c.Actor != null).Select(c => c.Actor).SingleOrDefault(o => o.IsPlayer);

    }
}
