using System;

namespace MattEland.Emergence.Model
{
    public struct Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public Position Add(Position pos) => new Position(X + pos.X, Y + pos.Y);
        public Position Add(int deltaX, int deltaY) => new Position(X + deltaX, Y + deltaY);
        public Position Subtract(Position pos) => new Position(X - pos.X, Y - pos.Y);
        public Position Subtract(int deltaX, int deltaY) => new Position(X - deltaX, Y - deltaY);

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Position other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Position left, Position right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Position left, Position right)
        {
            return !left.Equals(right);
        }

        public override string ToString() => $"({X}, {Y})";

        public Position GetNeighbor(MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return Add(0, -1);
                case MoveDirection.Right:
                    return Add(1, 0);
                case MoveDirection.Down:
                    return Add(0, 1);
                case MoveDirection.Left:
                    return Add(-1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
