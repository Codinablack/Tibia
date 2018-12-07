namespace Tibia.Network.Game.Packets
{
    public class BattleListLookPacket : IClientPacket
    {
        /// <summary>
        ///     Gets or sets the creature identifier.
        /// </summary>
        /// <value>
        ///     The creature identifier.
        /// </value>
        public uint CreatureId { get; set; }

        /// <summary>
        ///     Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The packet.</returns>
        public static BattleListLookPacket Parse(NetworkMessage message)
        {
            return new BattleListLookPacket
            {
                CreatureId = message.ReadUInt32()
            };
        }
    }
}