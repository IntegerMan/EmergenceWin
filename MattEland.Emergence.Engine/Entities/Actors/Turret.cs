using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Turret : Actor
    {
        public Turret(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "Turret";
        public override char AsciiChar => 'T';
    }
}