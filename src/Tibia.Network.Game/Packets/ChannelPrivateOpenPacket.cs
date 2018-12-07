namespace Tibia.Network.Game.Packets
{
    public class ChannelPrivateOpenPacket : IServerPacket, IClientPacket
    {
        /// <summary>
        ///     Gets or sets the name of the receiver.
        /// </summary>
        /// <value>
        ///     The name of the receiver.
        /// </value>
        public string ReceiverName { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static ChannelPrivateOpenPacket Parse(NetworkMessage message)
        {
            ChannelPrivateOpenPacket packet = new ChannelPrivateOpenPacket();
            packet.ReceiverName = message.ReadString();
            return packet;
        }

        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="receiverName">Name of the receiver.</param>
        public static void Add(NetworkMessage message, string receiverName)
        {
            message.AddPacketType(GamePacketType.ChannelPrivateOpen);
            message.AddString(receiverName);
        }
    }
}