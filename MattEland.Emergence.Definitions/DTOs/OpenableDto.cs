﻿using System.Diagnostics;

namespace MattEland.Emergence.Definitions.DTOs
{
    [DebuggerDisplay("(Openable: Pos:{Pos.SerializedValue} Type:{Type} Id:{Id} Open: {IsOpen})")]
    public class OpenableDto : GameObjectDto
    {

        /// <summary>
        /// Gets or sets a value indicating whether this object is open.
        /// </summary>
        /// <value><c>true</c> if this object is open; otherwise, <c>false</c>.</value>
        public bool IsOpen { get; set; }

    }
}