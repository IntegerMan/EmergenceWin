using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Door : WorldObject, IInteractive
    {
        public Door(Position pos) : base(pos, Guid.NewGuid())
        {
        }

        public bool IsOpen { get; set; }

        public override char AsciiChar => IsOpen ? ':' : '+';

        public override string ForegroundColor => GameColors.White;

        public override int ZIndex => 65;
        public IEnumerable<GameMessage> Interact(Actor actor)
        {
            if (IsOpen)
            {
                yield return MoveObject(actor, Pos);
            }
            else
            {
                IsOpen = true;
                yield return new ObjectUpdatedMessage(this);
            }
        }
    }
}