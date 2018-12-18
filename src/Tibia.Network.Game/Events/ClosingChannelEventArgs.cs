using System.ComponentModel;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class ClosingChannelEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.Events.ClosingChannelEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="channel">The channel.</param>
        public ClosingChannelEventArgs(ICharacterSpawn characterSpawn, IChannel channel)
        {
            CharacterSpawn = characterSpawn;
            Channel = channel;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public ICharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the channel.
        /// </summary>
        /// <value>
        ///     The channel.
        /// </value>
        public IChannel Channel { get; }
    }
}