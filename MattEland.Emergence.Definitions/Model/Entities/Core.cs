using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class Core : WorldObject, IInteractive
    {
        public Core(Pos2D pos) : base(pos, Guid.NewGuid())
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