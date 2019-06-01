using MattEland.Emergence.Engine.DTOs;
using MattEland.Emergence.Engine.Level;

namespace MattEland.Emergence.Engine.Entities.Actors
{
    public class LogicBomb : Bug
    {
        public override string Name => "Logic Bomb";
        public override char AsciiChar => 'l';

        public LogicBomb(Pos2D pos) : base(pos)
        {
        }

    }
}