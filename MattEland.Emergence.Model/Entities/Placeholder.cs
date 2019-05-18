using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Placeholder : WorldObject, IInteractive
    {
        private readonly char _display;

        public Placeholder(Position pos, char display) : base(pos, Guid.NewGuid())
        {
            if (display <= 0) throw new ArgumentOutOfRangeException(nameof(display));
            
            _display = display;
        }

        public override string ForegroundColor
        {
            get { return GameColors.Red; }
        }

        public override char AsciiChar => _display;

        public override int ZIndex => 30;
        
        public void Interact(ICommandContext context) => context.DisplayText("I don't know what this character is.");
    }
}