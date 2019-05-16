using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Stairs : WorldObject, IInteractive
    {
        public Stairs(Position pos, bool isDown) : base(pos, Guid.NewGuid())
        {
            IsDown = isDown;
        }

        public bool IsDown { get; }
        
        public override char AsciiChar => IsDown ? '<' : '>';
        public override int ZIndex => 70;

        public override string ForegroundColor => GameColors.White;
       
        public void Interact(ICommandContext context)
        {
            if (IsDown)
            {
                context.DisplayText("Moving on to the next level is not yet implemented.");
            }
            else
            {
                context.DisplayText("There's no turning back. I have to keep moving forward.");
            }
        }
    }
}