﻿using System;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class RequestedAppearanceWindowEventArgs : EventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.RequestedAppearanceWindowEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public RequestedAppearanceWindowEventArgs(CharacterSpawn characterSpawn)
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