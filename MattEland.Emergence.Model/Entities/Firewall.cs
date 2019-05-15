using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Firewall : WorldObject, IInteractive
    {
        public Firewall(Position pos) : base(pos, Guid.NewGuid())
        {
        }

        public override string ForegroundColor => IsOpen ? GameColors.LightGreen : GameColors.Orange;

        public bool IsOpen { get; set; }

        public override char AsciiChar => IsOpen ? ':' : '|';

        public override int ZIndex => 50;
        public IEnumerable<GameMessage> Interact(Actor actor)
        {
            if (IsOpen)
            {
                actor.Pos = Pos;
                yield return  new ObjectUpdatedMessage(actor);
            }
            else
            {
                yield return new DisplayTextMessage("The firewall is still up.");
            }
        }
    }
}