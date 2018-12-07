using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ChannelEventPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="channelName">Name of the channel.</param>
        /// <param name="channelEventType">Type of the channel event.</param>
        public static void Add(NetworkMessage message, ushort channelId, string channelName, ChannelEventType channelEventType)
        {
            message.AddPacketType(GamePacketType.ChannelEvent);
            message.AddUInt16(channelId);
            message.AddString(channelName);
            message.AddChannelEventType(channelEventType);
        }
    }
}