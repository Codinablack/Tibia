namespace Tibia.Network.Game.Packets
{
    public class PartySwitchSharedExperiencePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static PartySwitchSharedExperiencePacket Parse(NetworkMessage message)
        {
            return new PartySwitchSharedExperiencePacket
            {
                IsActive = message.ReadBoolean()
            };
        }
    }
}