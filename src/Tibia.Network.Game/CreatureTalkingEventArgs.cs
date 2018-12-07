using System.ComponentModel;
using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game
{
    public class CreatureTalkingEventArgs : CancelEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Network.Game.CharacterTalkingEventArgs" /> class.
        /// </summary>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="speechType">Type of the speech.</param>
        /// <param name="receiver">The receiver.</param>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="message">The message.</param>
        public CreatureTalkingEventArgs(CharacterSpawn characterSpawn, SpeechType speechType, string receiver, ushort channelId, string message)
        {
            CharacterSpawn = characterSpawn;
            SpeechType = speechType;
            Receiver = receiver;
            ChannelId = channelId;
            Message = message;
        }

        /// <summary>
        ///     Gets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public CharacterSpawn CharacterSpawn { get; }

        /// <summary>
        ///     Gets the type of the speech.
        /// </summary>
        /// <value>
        ///     The type of the speech.
        /// </value>
        public SpeechType SpeechType { get; }

        /// <summary>
        ///     Gets the receiver.
        /// </summary>
        /// <value>
        ///     The receiver.
        /// </value>
        public string Receiver { get; }

        /// <summary>
        ///     Gets the channel identifier.
        /// </summary>
        /// <value>
        ///     The channel identifier.
        /// </value>
        public ushort ChannelId { get; }

        /// <summary>
        ///     Gets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; }
    }
}