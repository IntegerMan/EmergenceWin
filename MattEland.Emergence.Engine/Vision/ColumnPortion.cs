namespace MattEland.Emergence.Engine.Vision
{
    public struct ColumnPortion
    {
        public int X { get; }
        public DirectionVector BottomVector { get; }
        public DirectionVector TopVector { get; }

        public ColumnPortion(int x, DirectionVector bottom, DirectionVector top)
            : this()
        {
            X = x;
            BottomVector = bottom;
            TopVector = top;
        }
    }
}