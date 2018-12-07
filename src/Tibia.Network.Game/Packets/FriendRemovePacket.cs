namespace Tibia.Network.Game.Packets
{
    public class FriendRemovePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the friend identifier.
        /// </summary>
        /// <value>
        ///     The friend identifier.
        /// </value>
        public uint FriendId { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static FriendRemovePacket Parse(NetworkMessage message)
        {
            return new FriendRemovePacket
            {
                FriendId = message.ReadUInt32()
            };
        }
    }
}