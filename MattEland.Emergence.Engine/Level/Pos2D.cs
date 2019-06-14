using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using MattEland.Emergence.Engine.Model;

namespace MattEland.Emergence.Engine.Level
{
    /// <summary>
    /// Represents a point in two dimensional space with integer X and Y coordinates.
    /// This is an immutable data structure. Operations will only generate new instances, not modify the
    /// current instance.
    /// </summary>
    [DebuggerDisplay("{X},{Y}")]
    public struct Pos2D : IEquatable<Pos2D>
    {
        private static readonly ConcurrentDictionary<int, ConcurrentDictionary<int, double>> Calcs =
            new ConcurrentDictionary<int, ConcurrentDictionary<int, double>>();

        private readonly int _x;
        private readonly int _y;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pos2D"/> struct with X and Y values matching the parameters.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public Pos2D(int x, int y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Gets the X coordinate.
        /// </summary>
        /// <value>The X coordinate.</value>
        public int X => _x;

        /// <summary>
        /// Gets the Y coordinate.
        /// </summary>
        /// <value>The Y coordinate.</value>
        public int Y => _y;

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(Pos2D other)
        {
            return _x == other._x && _y == other._y;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns><c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }

            return obj is Pos2D d && Equals(d);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                return (_x * 397) ^ _y;
            }
        }

        /// <summary>
        /// Implements the == operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Pos2D left, Pos2D right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the != operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Pos2D left, Pos2D right)
        {
            return !left.Equals(right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Concat("{", _x, " ,", _y, "}");
        }

        /// <summary>
        /// Gets a serializable value for the position of an object
        /// </summary>
        public string SerializedValue => string.Concat(_x, ",", _y);

        /// <summary>
        /// Adds the specified values to the position and returns a new position.
        /// </summary>
        /// <param name="xDelta">The x delta.</param>
        /// <param name="yDelta">The y delta.</param>
        /// <returns>Pos2D.</returns>
        public Pos2D Add(int xDelta, int yDelta)
        {
            return new Pos2D(_x + xDelta, _y + yDelta);
        }

        public double CalculateDistanceFrom(Pos2D point)
        {
            // Work in absolute values relative to 0,0
            var xDiff = Math.Abs(_x - point._y);
            var yDiff = Math.Abs(_y - point._y);

            // Check to see if we have a cached value already
            if (Calcs.TryGetValue(xDiff, out var yDict) && yDict.TryGetValue(yDiff, out var val))
            {
                return val;
            }

            // Make the calculation
            var distance = Math.Sqrt(xDiff * xDiff + yDiff * yDiff);

            // Cache the result
            if (yDict == null)
            {
                Calcs[xDiff] = new ConcurrentDictionary<int, double>
                {
                    [yDiff] = distance
                };
            }
            else
            {
                yDict[yDiff] = distance;
            }

            // Return the result, now that it's in cache for next time
            return distance;
        }

        public Pos2D GetNeighbor(MoveDirection direction, int spacesOver = 1)
        {
            switch (direction)
            {
                case MoveDirection.Up: return Add(0, -spacesOver);
                case MoveDirection.Right: return Add(spacesOver, 0);
                case MoveDirection.Down: return Add(0, spacesOver);
                case MoveDirection.Left: return Add(-spacesOver, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        public bool IsAdjacentTo(Pos2D pos) =>
            (pos.X == X && Math.Abs(pos.Y - Y) == 1) || 
            (pos.Y == Y && Math.Abs(pos.X - X) == 1);
    }
}