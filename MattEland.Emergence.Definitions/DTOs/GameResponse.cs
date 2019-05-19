﻿using System;
using System.Collections.Generic;

namespace MattEland.Emergence.Definitions.DTOs
{
    /// <summary>
    /// Represents the system's response to the player's requested move, including the current game state and any messages or effects to apply.
    /// </summary>
    public sealed class GameResponse
    {
        /// <summary>
        /// Gets or sets the unique identifier for the response
        /// </summary>
        public Guid UID { get; set; }

        /// <summary>
        /// Gets or sets the current state of the game.
        /// </summary>
        /// <value>The state of the game.</value>
        public GameState State { get; set; } = new GameState();

        /// <summary>
        /// Gets or sets the messages the client application should display.
        /// </summary>
        /// <value>The messages.</value>
        public ICollection<ClientMessage> Messages { get; set; } = new List<ClientMessage>();

        /// <summary>
        /// Gets or sets the effects that took place this turn that should be rendered client-side
        /// </summary>
        public ICollection<EffectDto> Effects { get; set; } = new List<EffectDto>();
    }
}