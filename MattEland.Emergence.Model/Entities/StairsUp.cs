using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class StairsUp : WorldObject, IInteractive
    {
        public StairsUp(Position pos) : base(pos, Guid.NewGuid())
        {
        }

        public override char AsciiChar => '>';
        public override int ZIndex => 70;

        public override string ForegroundColor => GameColors.White;


        public IEnumerable<GameMessage> Interact(Actor actor)
        {
            yield return new DisplayTextMessage("There's no turning back. I have to keep moving forward.");
        }
    }
}