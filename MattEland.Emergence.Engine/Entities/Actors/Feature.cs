using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Feature : Bug
    {
        public Feature(Pos2D pos) : base(pos)
        {
            Team = Alignment.Bug;
        }

        public override string Name => "Feature";
        public override char AsciiChar => 'f';
    }
}