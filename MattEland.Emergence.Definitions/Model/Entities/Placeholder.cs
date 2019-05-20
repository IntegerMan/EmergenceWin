using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Placeholder : WorldObject, IInteractive
    {
        private readonly char _display;

        public Placeholder(Pos2D pos, char display) : base(pos, Guid.NewGuid())
        {
            if (display <= 0) throw new ArgumentOutOfRangeException(nameof(display));
            
            _display = display;
        }

        public override string ForegroundColor => GameColors.Red;

        public override char AsciiChar => _display;

        public override int ZIndex => 30;
        
        public void Interact(ICommandContext context) => context.DisplayText("I don't know what this character is.");
    }
}