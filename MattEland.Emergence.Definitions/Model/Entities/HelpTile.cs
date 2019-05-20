using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class HelpTile : WorldObject, IInteractive
    {
        public HelpTile(Pos2D pos, string text) : base(pos, Guid.NewGuid())
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