using Tibia.Data;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class PrivateMessagePacket : IPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="type">The type.</param>
        /// <param name="statementId">The statement identifier.</param>
        /// <param name="text">The text.</param>
        public static void Add(NetworkMessage message, CharacterSpawn characterSpawn, SpeechType type, uint statementId, string text)
        {
            message.AddPacketType(GamePacketType.CreatureSpeech);
            message.AddUInt32(statementId);

            if (characterSpawn != null)
            {
                message.AddString(characterSpawn.Character.Name);
                message.AddUInt16((ushort) characterSpawn.Level.Current);
            }
            else
            {
                // TODO: Is this a magic number?
                message.AddUInt32(0x00);
            }

            message.AddSpeechType(type);
            message.AddString(text);
        }
    }
}