namespace Tibia.Network.Game.Packets
{
    public class FriendEditPacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the friend identifier.
        /// </summary>
        /// <value>
        ///     The friend identifier.
        /// </value>
        public uint FriendId { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the icon.
        /// </summary>
        /// <value>
        ///     The icon.
        /// </value>
        public uint Icon { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [notify on login].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [notify on login]; otherwise, <c>false</c>.
        /// </value>
        public bool NotifyOnLogin { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static FriendEditPacket Parse(NetworkMessage message)
        {
            return new FriendEditPacket
            {
                FriendId = message.ReadUInt32(),
                Description = message.ReadString(),
                Icon = message.ReadUInt32(),
                NotifyOnLogin = message.ReadBoolean()
            };
        }
    }
}