using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Door : WorldObject, IInteractive
    {
        public Door(Pos2D pos) : base(pos, Guid.NewGuid())
        {
        }

        public bool IsOpen { get; set; }

        public override char AsciiChar => IsOpen ? ':' : '+';

        public override string ForegroundColor => GameColors.White;

        public override int ZIndex => 65;
        public void Interact(ICommandContext context)
        {
            if (IsOpen)
            {
                context.MoveExecutingActor(Pos);
            }
            else
            {
                IsOpen = true;
                context.UpdateObject(this);
            }
        }
    }
}