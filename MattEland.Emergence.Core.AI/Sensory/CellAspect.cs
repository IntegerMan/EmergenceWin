namespace MattEland.Emergence.AI.Sensory
{
    public struct CellAspect
    {
        public CellAspect(CellAspectType id, decimal value)
        {
            Id = id;
            Value = value;
        }

        public CellAspectType Id { get; }
        public decimal Value { get; }
    }
}