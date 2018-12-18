using System.ComponentModel;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class SendingChannelListEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.SendingChannelListEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        public SendingChannelListEventArgs(ICharacterSpawn characterSpawn)
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