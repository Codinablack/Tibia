using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class CharacterSpeechPacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the channel identifier.
        /// </summary>
        /// <value>
        ///     The channel identifier.
        /// </value>
        public ushort ChannelId { get; set; }

        /// <summary>
        ///     Gets or sets the type of the speech.
        /// </summary>
        /// <value>
        ///     The type of the speech.
        /// </value>
        public SpeechType SpeechType { get; set; }

        /// <summary>
        ///     Gets or sets the receiver.
        /// </summary>
        /// <value>
        ///     The receiver.
        /// </value>
        public string Receiver { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static CharacterSpeechPacket Parse(NetworkMessage message)
        {
            CharacterSpeechPacket packet = new CharacterSpeechPacket();

            packet.SpeechType = (SpeechType) message.ReadByte();

            // TODO: This could use OOP with inheritance
            switch (packet.SpeechType)
            {
                case SpeechType.PrivateTo:
                case SpeechType.PrivateRedTo:
                    packet.Receiver = message.ReadString();
                    break;
                case SpeechType.Yellow:
                case SpeechType.Red:
                    packet.ChannelId = message.ReadUInt16();
                    break;
            }

            packet.Message = message.ReadString();
            return packet;
        }
    }
}