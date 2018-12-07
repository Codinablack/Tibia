using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class FriendStatusPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="friendId">The friend identifier.</param>
        /// <param name="status">The status.</param>
        public static void Add(NetworkMessage message, uint friendId, SessionStatus status)
        {
            message.AddPacketType(GamePacketType.FriendStatus);
            message.AddUInt32(friendId);
            message.AddSessionStatus(status);
        }
    }
}