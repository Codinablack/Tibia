namespace Tibia.Network.Game.Packets
{
    public class DisconnectPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="reason">The reason.</param>
        public static void Add(NetworkMessage message, string reason)
        {
            message.AddPacketType(GamePacketType.Disconnect);
            message.AddString(reason);
        }
    }
}