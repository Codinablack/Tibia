﻿using System.ComponentModel;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class EnteringGameEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.EnteringGameEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public EnteringGameEventArgs(CharacterSpawn characterSpawn)
        {
            CharacterSpawn = characterSpawn;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }
    }
}