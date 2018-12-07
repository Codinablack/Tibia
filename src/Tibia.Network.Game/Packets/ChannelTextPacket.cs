using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class ChannelTextPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        /// <param name="channelId">The channel identifier.</param>
        /// <param name="type">The type.</param>
        /// <param name="statementId">The statement identifier.</param>
        /// <param name="text">The text.</param>
        public static void Add(NetworkMessage message, ICreatureSpawn creatureSpawn, ushort channelId, SpeechType type, uint statementId, string text)
        {
            message.AddPacketType(GamePacketType.CreatureSpeech);
            message.AddUInt32(statementId);

            if (creatureSpawn == null)
                message.AddUInt32(0x00);
            else if (type == SpeechType.ChannelR2)
            {
                message.AddUInt32(0x00);
                type = SpeechType.Red;
            }
            else
            {
                message.AddString(creatureSpawn.Creature.Name);

                // Add level only for characters
                if (creatureSpawn is ICharacterSpawn characterSpawn)
                    message.AddUInt16((ushort) characterSpawn.Level.Current);
                else
                    message.AddUInt16(0x00);
            }

            message.AddSpeechType(type);
            message.AddUInt16(channelId);
            message.AddString(text);
        }
    }
}