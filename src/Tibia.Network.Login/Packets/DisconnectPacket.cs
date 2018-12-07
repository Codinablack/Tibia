namespace Tibia.Network.Login.Packets
{
    internal class DisconnectPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="reason">The reason.</param>
        public static void Add(NetworkMessage message, string reason)
        {
            message.AddPacketType(LoginPacketType.Disconnect);
            message.AddString(reason);
        }
    }
}