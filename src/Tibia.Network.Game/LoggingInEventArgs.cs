﻿using System.ComponentModel;

namespace Tibia.Network.Game
{
    public class LoggingInEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.LoggingInEventArgs" /> class.
        /// </summary>
        /// <param name="characterName">Name of the character.</param>
        public LoggingInEventArgs(string characterName)
        {
            CharacterName = characterName;
        }

        /// <summary>
        ///     Gets the name of the character.
        /// </summary>
        /// <value>
        ///     The name of the character.
        /// </value>
        public string CharacterName { get; }
    }
}