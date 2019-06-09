using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class SecurityAgent : Actor
    {
        public SecurityAgent(Pos2D pos) : base(pos)
        {
            Team = Alignment.SystemSecurity;
        }

        public override string Name => "Security Agent";
        public override char AsciiChar => 's';
    }
}