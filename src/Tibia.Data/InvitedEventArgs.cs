using System;

namespace Tibia.Data
{
    public class InvitedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvitedEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public InvitedEventArgs(ICharacterSpawn characterSpawn)
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