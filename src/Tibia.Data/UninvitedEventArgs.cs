using System;

namespace Tibia.Data
{
    public class UninvitedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UninvitedEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public UninvitedEventArgs(ICharacterSpawn characterSpawn)
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