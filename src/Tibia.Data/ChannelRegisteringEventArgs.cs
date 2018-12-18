using System.ComponentModel;

namespace Tibia.Data
{
    public class ChannelRegisteringEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Data.ChannelRegisteringEventArgs" />
        ///     class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public ChannelRegisteringEventArgs(ICharacterSpawn characterSpawn)
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