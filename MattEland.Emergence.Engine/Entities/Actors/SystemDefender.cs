using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class SystemDefender : Actor
    {
        public SystemDefender(Pos2D pos) : base(pos)
        {
            Team = Alignment.SystemSecurity;
        }

        public override string Name => "System Defender";
        public override char AsciiChar => 'D';
    }
}