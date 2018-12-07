namespace Tibia.Network.Login.Packets
{
    internal class SessionKeyPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="userName">The user name.</param>
        /// <param name="password">The password.</param>
        public static void Add(NetworkMessage message, string userName, string password)
        {
            string sessionKey = $"{userName}\n{password}";

            message.AddPacketType(LoginPacketType.SessionKey);
            message.AddString(sessionKey);
        }
    }
}