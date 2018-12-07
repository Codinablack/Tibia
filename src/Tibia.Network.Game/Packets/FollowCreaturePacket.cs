namespace Tibia.Network.Game.Packets
{
    public class FollowCreaturePacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the creature identifier.
        /// </summary>
        /// <value>
        ///     The creature identifier.
        /// </value>
        public uint CreatureId { get; set; }

        /// <summary>
        ///     Gets or sets the creature identifier authentication.
        /// </summary>
        /// <value>
        ///     The creature identifier authentication.
        /// </value>
        public uint CreatureIdAuthentication { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static FollowCreaturePacket Parse(NetworkMessage message)
        {
            return new FollowCreaturePacket
            {
                CreatureId = message.ReadUInt32(),
                CreatureIdAuthentication = message.ReadUInt32()
            };
        }
    }
}