namespace Tibia.Network.Game.Packets
{
    public class PendingStateEnteredPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void Add(NetworkMessage message)
        {
            message.AddPacketType(GamePacketType.PendingStateEntered);
        }
    }
}