using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Glitch : Bug
    {
        public Glitch(Pos2D pos) : base(pos)
        {
            Team = Alignment.Bug;
        }

        public override string Name => "Glitch";
        public override char AsciiChar => 'g';
    }
}