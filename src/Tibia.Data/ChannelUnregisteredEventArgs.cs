using System;

namespace Tibia.Data
{
    public class ChannelUnregisteredEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ChannelUnregisteredEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <inheritdoc />
        public ChannelUnregisteredEventArgs(ICharacterSpawn characterSpawn)
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