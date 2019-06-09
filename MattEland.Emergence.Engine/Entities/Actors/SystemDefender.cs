using MattEland.Emergence.Engine.Level;
using MattEland.Emergence.Engine.Services;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class SystemDefender : Actor
    {
        public SystemDefender(Pos2D pos) : base(pos)
        {
        }

        public override string Name => "System Defender";
        public override char AsciiChar => 'D';

        public override int Strength => 3;
        public override int Defense => 2;
        public override int Accuracy => 50;
        public override int Evasion => 30;
        protected override void InitializeProtected()
        {
            base.InitializeProtected();

            Team = Alignment.SystemSecurity;
            MaxStability = 5;
            MaxOperations = 15;
        }
        
    }
    
    
    
}