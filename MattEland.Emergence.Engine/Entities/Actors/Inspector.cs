using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Inspector : Actor
    {
        public Inspector(Pos2D pos) : base(pos)
        {
            Team = Alignment.SystemCore;
        }

        public override string Name => "Inspector";
        public override char AsciiChar => 'i';
    }
}