namespace Tibia.Network.Game.Packets
{
    public class PartyRevokeInvitationPacket : IClientPacket
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
        public static PartyRevokeInvitationPacket Parse(NetworkMessage message)
        {
            return new PartyRevokeInvitationPacket
            {
                TargetCharacterId = message.ReadUInt32()
            };
        }
    }
}