using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class HelpTile : WorldObject, IInteractive
    {
        public HelpTile(Position pos, string text) : base(pos, Guid.NewGuid())
        {
            Text = text;
        }

        public string Text { get; set; }

        public override char AsciiChar => '?';
        public override int ZIndex => 70;

        public override string ForegroundColor => GameColors.White;
        public override string BackgroundColor => GameColors.Blue;
        
        
        public void Interact(ICommandContext context) => context.DisplayText(Text);
    }
}