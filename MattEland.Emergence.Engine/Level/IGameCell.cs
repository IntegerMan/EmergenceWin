using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace MattEland.Emergence.Engine.Level
{
    public interface IGameCell
    {
        /// <summary>
        /// Gets or sets the absolute position of the cell within the level.
        /// </summary>
        /// <value>The position of the cell.</value>
        Pos2D Pos { get; set; }

        /// <summary>
        /// Gets or sets the type of the floor for the cell.
        /// </summary>
        /// <value>The type of the floor.</value>
        FloorType FloorType { get; set; }

        /// <summary>
        /// Gets the objects present in the cell.
        /// </summary>
        /// <value>The objects present in the cell.</value>
        IEnumerable<IGameObject> Objects { get; }

        /// <summary>
        /// Gets a value indicating whether or not this cell has a blocking obstacle.
        /// </summary>
        /// <value><c>true</c> if this instance has an object blocking movement; otherwise, <c>false</c>.</value>
        bool HasObstacle { get; }

        /// <summary>
        /// Gets a value indicating whether or not this cell has a blocking obstacle, excluding actors.
        /// </summary>
        /// <value><c>true</c> if this instance has a non-actor object blocking movement; otherwise, <c>false</c>.</value>
        bool HasNonActorObstacle { get; }

        [CanBeNull]
        IActor Core { get; }

        /// <summary>
        /// Adds an object to the cell.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        void AddObject(IGameObject obj);

        /// <summary>
        /// Removes all objects from the cell that match the specified matcher function.
        /// </summary>
        /// <param name="matcherFunc">The matcher function.</param>
        void RemoveAllObjects(Func<IGameObject, bool> matcherFunc);

        bool RemoveObject(IGameObject gameObject);

        /// <summary>
        /// Gets the actor in the cell
        /// </summary>
        /// <returns>The actor, if one is present</returns>
        [CanBeNull]
        IActor Actor { get; }

        bool BlocksSight { get; }

        /// <summary>
        /// Gets or sets the corruption count in the given cell.
        /// </summary>
        int Corruption { get; set; }
    }
}