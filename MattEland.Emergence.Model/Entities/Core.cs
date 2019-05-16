using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class Core : WorldObject, IInteractive
    {
        public Core(Position pos) : base(pos, Guid.NewGuid())
        {
        }

        public override char AsciiChar => 'C';
        public override int ZIndex => 70;

        public bool IsCaptured { get; private set; }

        public override string ForegroundColor => IsCaptured ? GameColors.Green : GameColors.Yellow;

        public void Interact(ICommandContext context)
        {
            if (!IsCaptured)
            {
                IsCaptured = true;
                context.UpdateObject(this);
                context.UpdateCapturedCores();
            }
        }
    }
}