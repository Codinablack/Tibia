using System;

namespace Tibia.Network.Game.Packets
{
    public class GameServerConnectPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="onlineTime">The online time.</param>
        public static void Add(NetworkMessage message, TimeSpan onlineTime)
        {
            message.AddPacketType(GamePacketType.Connect);
            message.AddUInt32((uint) onlineTime.Seconds);
            // TODO: Research this byte. Is it fractional time?
            message.AddByte(0x10);
        }
    }
}