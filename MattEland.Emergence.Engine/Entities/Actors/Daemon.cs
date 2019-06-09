using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class Daemon : Actor
    {
        public Daemon(Pos2D pos) : base(pos)
        {
        }

        public override int Strength => 3;
        public override int Accuracy => 70;
        public override int Evasion => 5;
        
        protected override void InitializeProtected()
        {
            base.InitializeProtected();
            
            Team = Alignment.SystemSecurity;
        }

        public override string Name => "Daemon";
        public override char AsciiChar => 'd';
    }
}