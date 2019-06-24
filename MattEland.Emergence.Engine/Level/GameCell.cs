using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using MattEland.Emergence.Engine.Entities;
using MattEland.Emergence.Engine.Entities.Actors;
using MattEland.Emergence.Engine.Game;
using MattEland.Emergence.Engine.Model;
using MattEland.Shared.Collections;

namespace MattEland.Emergence.Engine.Level
{
    /// <summary>
    /// Contains information on a single cell within the level
    /// </summary>
    [DebuggerDisplay("(Cell: {Pos} {FloorType}, Actor: {Actor})")]
    public class GameCell
    {
        [ItemNotNull] [NotNull]
        private readonly ICollection<GameObjectBase> _objects;
        private int _corruption;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameCell"/> class.
        /// </summary>
        public GameCell()
        {
            _objects = new List<GameObjectBase>();
        }

        /// <summary>
        /// Gets or sets the absolute position of the cell within the level.
        /// </summary>
        /// <value>The position of the cell.</value>
        public Pos2D Pos { get; set; }

        /// <summary>
        /// Gets or sets the type of the floor for the cell.
        /// </summary>
        /// <value>The type of the floor.</value>
        public FloorType FloorType { get; set; }

        /// <summary>
        /// Gets the objects present in the cell.
        /// </summary>
        /// <value>The objects present in the cell.</value>
        public IEnumerable<GameObjectBase> Objects => _objects; // TODO: Only serialize this when there are objects present.

        /// <summary>
        /// Gets a value indicating whether or not this cell has a blocking obstacle.
        /// </summary>
        /// <value><c>true</c> if this instance has an object blocking movement; otherwise, <c>false</c>.</value>
        public bool HasObstacle => Objects.Any(o => IsBlockingObjectType(o.ObjectType, false));

        /// <summary>
        /// Gets a value indicating whether or not this cell has a blocking obstacle, excluding actors.
        /// </summary>
        /// <value><c>true</c> if this instance has a non-actor object blocking movement; otherwise, <c>false</c>.</value>
        public bool HasNonActorObstacle => Objects.Any(o => IsBlockingObjectType(o.ObjectType, true));

        /// <summary>
        /// Determines whether <paramref name="objectType"/> refers to a type of object that blocks movement into its associated cell.
        /// </summary>
        /// <param name="objectType">Type of object being evaluated.</param>
        /// <returns><c>true</c> if the object type blocks movement; otherwise, <c>false</c>.</returns>
        private static bool IsBlockingObjectType(GameObjectType objectType, bool excludeActors)
        {
            switch (objectType)
            {
                case GameObjectType.Core:
                case GameObjectType.Actor:
                case GameObjectType.Player:
                case GameObjectType.Turret:
                    return !excludeActors;

                case GameObjectType.DataStore:
                case GameObjectType.Divider:
                case GameObjectType.Entrance:
                case GameObjectType.Exit:
                case GameObjectType.Firewall:
                case GameObjectType.Service:
                case GameObjectType.Treasure:
                case GameObjectType.Wall:
                case GameObjectType.Water:
                case GameObjectType.Help:
                    return true;

                case GameObjectType.Floor:
                default:
                    return false;
            }
        }

        /// <summary>
        /// Adds an object to the cell.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public void AddObject(GameObjectBase obj)
        {
            _objects.Add(obj);

            if (obj is Actor actor)
            {
                Actor = actor;

                if (actor.ObjectType == GameObjectType.Core)
                {
                    Core = actor;
                }
            }

            obj.Pos = Pos;
        }

        /// <summary>
        /// Removes all objects from the cell that match the specified matcher function.
        /// </summary>
        /// <param name="matcherFunc">The matcher function.</param>
        public void RemoveAllObjects(Func<GameObjectBase, bool> matcherFunc)
        {
            var matches = _objects.Where(matcherFunc);

            foreach (var match in matches.ToList())
            {
                _objects.Remove(match);
            }

            if (Actor != null && matcherFunc(Actor))
            {
                Actor = null;

                if (Core != null && matcherFunc(Core))
                {
                    Core = null;
                }
            }
        }

        public bool RemoveObject(GameObjectBase gameObject)
        {
            if (Actor == gameObject)
            {
                Actor = null;

                if (Core == gameObject)
                {
                    Core = null;
                }
            }

            return _objects.Remove(gameObject);
        }

        [CanBeNull]
        public Actor Actor { get; private set; }

        public bool BlocksSight
        {
            get { return _objects.Any(o => o.BlocksSight); }
        }

        public int Corruption
        {
            get => _corruption;
            set => _corruption = Math.Max(0, Math.Min(3, value));
        }

        [CanBeNull]
        public Actor Core { get; private set; }

        public IEnumerable<GameCell> GetAdjacentCells([NotNull] GameContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            var directions = Enum.GetValues(typeof(MoveDirection)).Cast<MoveDirection>().ToList();

            var cells = new List<GameCell>(directions.Count);

            directions.Each(d =>
            {
                var cell = context.Level.GetCell(Pos.GetNeighbor(d));
                if (cell != null)
                {
                    cells.Add(cell);
                }
            });
            
            return cells;
        }
        
        public IEnumerable<GameCell> FilterAdjacentCells([NotNull] GameContext context, Func<IEnumerable<GameCell>, IEnumerable<GameCell>> filter)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            return filter(GetAdjacentCells(context));
        }
    }
}