using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class CreatureSpeechPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="type">The type.</param>
        /// <param name="statementId">The statement identifier.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        public static void Add(NetworkMessage message, ICreatureSpawn creatureSpawn, SpeechType type, uint statementId, string text, IVector3 position)
        {
            message.AddPacketType(GamePacketType.CreatureSpeech);
            message.AddUInt32(statementId);
            message.AddString(creatureSpawn.Creature.Name);

            // Add level only for characters
            if (creatureSpawn is ICharacterSpawn characterSpawn)
                message.AddUInt16((ushort) characterSpawn.Level.Current);
            else
                message.AddUInt16(0x00);

            message.AddSpeechType(type);
            message.AddVector3(position ?? creatureSpawn.Tile.Position);
            message.AddString(text);
        }
    }
}