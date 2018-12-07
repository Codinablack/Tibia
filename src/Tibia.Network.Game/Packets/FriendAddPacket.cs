namespace Tibia.Network.Game.Packets
{
    public class FriendAddPacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the name of the friend.
        /// </summary>
        /// <value>
        ///     The name of the friend.
        /// </value>
        public string FriendName { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static FriendAddPacket Parse(NetworkMessage message)
        {
            return new FriendAddPacket
            {
                FriendName = message.ReadString()
            };
        }
    }
}