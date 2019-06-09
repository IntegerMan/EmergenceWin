using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class GarbageCollector : Actor
    {
        public GarbageCollector(Pos2D pos) : base(pos)
        {
            Team = Alignment.SystemSecurity;
        }

        public override string Name => "Garbage Collector";
        public override char AsciiChar => 'G';
    }
}