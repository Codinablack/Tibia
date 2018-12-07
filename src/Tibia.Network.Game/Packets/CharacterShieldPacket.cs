using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class CharacterShieldPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="partyShield">The party shield.</param>
        public static void Add(NetworkMessage message, CharacterSpawn characterSpawn, PartyShield partyShield)
        {
            message.AddPacketType(GamePacketType.CharacterShield);
            message.AddUInt32(characterSpawn.Id);
            message.AddPartyShield(partyShield);
        }
    }
}