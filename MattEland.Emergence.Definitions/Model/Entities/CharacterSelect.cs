using System;
using MattEland.Emergence.Definitions.Level;
using MattEland.Emergence.Definitions.Model.EngineDefinitions;

namespace MattEland.Emergence.Definitions.Model.Entities
{
    public class CharacterSelect : WorldObject, IInteractive
    {
        public CharacterSelect(Pos2D pos) : base(pos, Guid.NewGuid())
        {
        }

        public override string ForegroundColor => GameColors.LightGreen;

        public override char AsciiChar => '@';
        public override int ZIndex => 70;

        public void Interact(ICommandContext context) => context.DisplayText("Character Selection is not implemented");
    }
}