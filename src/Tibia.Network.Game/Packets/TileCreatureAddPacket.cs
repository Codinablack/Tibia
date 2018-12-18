using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class TileCreatureAddPacket : CreaturePacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="character">The character.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        /// <param name="creature">The creature.</param>
        /// <param name="known">if set to <c>true</c> [known].</param>
        /// <param name="removeKnown">The remove known.</param>
        public static void Add(NetworkMessage message, ICharacterSpawn character, IVector3 position, byte stackPosition, ICreatureSpawn creature, bool known, uint removeKnown)
        {
            message.AddPacketType(GamePacketType.TileAddArtifact);
            message.AddVector3(position);
            message.AddByte(stackPosition);
            AddCreature(message, character, creature, known, removeKnown);
        }
    }
}