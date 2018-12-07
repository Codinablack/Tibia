using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ChannelClosePacket : IClientPacket, IServerPacket
    {
        /// <summary>
        ///     Gets or sets the type of the channel.
        /// </summary>
        /// <value>
        ///     The type of the channel.
        /// </value>
        public ChannelType ChannelType { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static ChannelClosePacket Parse(NetworkMessage message)
        {
            ChannelClosePacket packet = new ChannelClosePacket();
            packet.ChannelType = message.ReadChannelType();
            return packet;
        }

        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channelId">The channel identifier.</param>
        public static void Add(NetworkMessage message, ushort channelId)
        {
            message.AddPacketType(GamePacketType.CloseChannel);
            message.AddUInt16(channelId);
        }

        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channelType">Type of the channel.</param>
        public static void Add(NetworkMessage message, ChannelType channelType)
        {
            message.AddPacketType(GamePacketType.CloseChannel);
            message.AddChannelType(channelType);
        }
    }
}