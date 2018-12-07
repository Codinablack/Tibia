namespace Tibia.Network.Login
{
    internal static class NetworkMessageExtensions
    {
        /// <summary>
        ///     Adds the type of the packet.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public static void AddPacketType(this NetworkMessage message, LoginPacketType type)
        {
            message.AddByte((byte) type);
        }
    }
}