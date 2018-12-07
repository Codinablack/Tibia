namespace Tibia.Network.Game.Packets
{
    public class MountTogglePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is riding.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is riding; otherwise, <c>false</c>.
        /// </value>
        public bool IsRiding { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static MountTogglePacket Parse(NetworkMessage message)
        {
            return new MountTogglePacket
            {
                IsRiding = message.ReadBoolean()
            };
        }
    }
}