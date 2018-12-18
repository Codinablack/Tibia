using System;
using Tibia.Spawns;

namespace Tibia.Network
{
    public class DisconnectedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.DisconnectedEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public DisconnectedEventArgs(CharacterSpawn characterSpawn)
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