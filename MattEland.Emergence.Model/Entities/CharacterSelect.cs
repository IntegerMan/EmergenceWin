﻿using System;
using System.Collections.Generic;
using MattEland.Emergence.Model.Messages;

namespace MattEland.Emergence.Model.Entities
{
    public class CharacterSelect : WorldObject, IInteractive
    {
        public CharacterSelect(Position pos) : base(pos, Guid.NewGuid())
        {
        }

        public override string ForegroundColor => GameColors.LightGreen;

        public override char AsciiChar => '@';
        public override int ZIndex => 70;

        public void Interact(ICommandContext context) => context.DisplayText("Character Selection is not implemented");
    }
}