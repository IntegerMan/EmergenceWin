using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Firewall : WorldObject, IInteractive
    {
        public Firewall(Pos2D pos) : base(pos, Guid.NewGuid())
        {
        }

        public override string ForegroundColor => IsOpen ? GameColors.LightGreen : GameColors.Orange;

        public bool IsOpen { get; set; }

        public override char AsciiChar => IsOpen ? ':' : '|';

        public override int ZIndex => 50;
        public void Interact(ICommandContext context)
        {
#if DEBUG
            context.MoveExecutingActor(Pos);
#else
            if (IsOpen)
            {
                context.MoveExecutingActor(Pos);
            }
            else
            {
                context.DisplayText("The firewall is still up.");
            }
#endif
        }
    }
}