using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Stairs : WorldObject, IInteractive
    {
        public Stairs(Pos2D pos, bool isDown) : base(pos, Guid.NewGuid())
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
                context.AdvanceToNextLevel();
            }
            else
            {
                context.DisplayText("There's no turning back. I have to keep moving forward.");
            }
        }
    }
}