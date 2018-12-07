namespace Tibia.Network.Game
{
    internal static class NetworkMessageExtensions
    {
        /// <summary>
        ///     Adds the type of the packet.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type.</param>
        public static void AddPacketType(this NetworkMessage message, GamePacketType type)
        {
            message.AddByte((byte) type);
        }

        /// <summary>
        ///     Reads the type of the game client packet.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet type.</returns>
        public static GameClientPacketType ReadGameClientPacketType(this NetworkMessage message)
        {
            return (GameClientPacketType) message.ReadByte();
        }
    }
}