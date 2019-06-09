using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Bit : Actor
    {
        public Bit(Pos2D pos) : base(pos)
        {
            Team = Alignment.SystemCore;
        }

        public override string Name => "Bit";
        public override char AsciiChar => '0';
    }
}