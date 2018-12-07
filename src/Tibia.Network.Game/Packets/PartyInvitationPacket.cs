namespace Tibia.Network.Game.Packets
{
    public class PartyInvitationPacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the target character identifier.
        /// </summary>
        /// <value>
        ///     The target character identifier.
        /// </value>
        public uint TargetCharacterId { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static PartyInvitationPacket Parse(NetworkMessage message)
        {
            return new PartyInvitationPacket
            {
                TargetCharacterId = message.ReadUInt32()
            };
        }
    }
}