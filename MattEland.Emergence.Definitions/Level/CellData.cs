﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace MattEland.Emergence.Definitions.Level
{
    /// <summary>
    /// Contains information on a single cell within the level
    /// </summary>
    [DebuggerDisplay("(Cell: Pos:{Pos.X},{Pos.Y} Floor:{FloorType})")]
    public class CellData : IGameCell
    {
        [ItemNotNull] [NotNull]
        private readonly ICollection<IGameObject> _objects;
        private int _corruption;

        /// <summary>
        /// Initializes a new instance of the <see cref="CellData"/> class.
        /// </summary>
        public CellData()
        {
            _objects = new List<IGameObject>();
        }

        /// <summary>
        /// Gets or sets the absolute position of the cell within the level.
        /// </summary>
        /// <value>The position of the cell.</value>
        [Required]
        public Pos2D Pos { get; set; }

        /// <summary>
        /// Gets or sets the type of the floor for the cell.
        /// </summary>
        /// <value>The type of the floor.</value>
        [Required]
        public FloorType FloorType { get; set; }

        /// <summary>
        /// Gets the objects present in the cell.
        /// </summary>
        /// <value>The objects present in the cell.</value>
        public IEnumerable<IGameObject> Objects => _objects; // TODO: Only serialize this when there are objects present.

        /// <summary>
        /// Gets a value indicating whether or not this cell has a blocking obstacle.
        /// </summary>
        /// <value><c>true</c> if this instance has an object blocking movement; otherwise, <c>false</c>.</value>
        [JsonIgnore]
        public bool HasObstacle => Objects.Any(o => IsBlockingObjectType(o.ObjectType, false));

        /// <summary>
        /// Gets a value indicating whether or not this cell has a blocking obstacle, excluding actors.
        /// </summary>
        /// <value><c>true</c> if this instance has a non-actor object blocking movement; otherwise, <c>false</c>.</value>
        [JsonIgnore]
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

                default:
                    return false;
            }
        }

        /// <summary>
        /// Adds an object to the cell.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        public void AddObject(IGameObject obj)
        {
            _objects.Add(obj);

            if (obj is IActor actor)
            {
                Actor = actor;

                if (actor.ObjectType == GameObjectType.Core)
                {
                    Core = actor;
                }
            }

            obj.Position = Pos;
        }

        /// <summary>
        /// Removes all objects from the cell that match the specified matcher function.
        /// </summary>
        /// <param name="matcherFunc">The matcher function.</param>
        public void RemoveAllObjects(Func<IGameObject, bool> matcherFunc)
        {
            var matches = _objects.Where(matcherFunc);

            foreach (var match in matches.ToList())
            {
                _objects.Remove(match);
            }

            if (Actor != null && matcherFunc(Actor))
            {
                Actor = null;
            }

            if (Core != null && matcherFunc(Core))
            {
                Core = null;
            }
            
        }

        public bool RemoveObject(IGameObject gameObject)
        {
            if (Core == gameObject)
            {
                Core = null;
            }

            if (Actor == gameObject)
            {
                Actor = null;
            }

            return _objects.Remove(gameObject);
        }

        [CanBeNull]
        public IActor Actor { get; private set; }

        public bool BlocksSight
        {
            get { return _objects.Any(o => o.BlocksSight); }
        }

        /// <inheritdoc />
        public int Corruption
        {
            get => _corruption;
            set => _corruption = Math.Max(0, Math.Min(3, value));
        }

        [CanBeNull]
        public IActor Core { get; private set; }

    }
}