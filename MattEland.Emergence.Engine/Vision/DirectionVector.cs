namespace MattEland.Emergence.Engine.Vision
{
    public struct DirectionVector
    {
        public int X { get; }
        public int Y { get; }

        public DirectionVector(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }
    }
}