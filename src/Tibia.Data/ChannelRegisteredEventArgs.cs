using System;

namespace Tibia.Data
{
    public class ChannelRegisteredEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelRegisteredEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public ChannelRegisteredEventArgs(ICharacterSpawn characterSpawn)
        {
            CharacterSpawn = characterSpawn;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public ICharacterSpawn CharacterSpawn { get; }
    }
}