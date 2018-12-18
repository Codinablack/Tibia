using System;
using Tibia.Spawns;

namespace Tibia.Network.Game.Events
{
    public class LoggedInEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.LoggedInEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public LoggedInEventArgs(CharacterSpawn characterSpawn)
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