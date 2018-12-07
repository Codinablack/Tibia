using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class SelfAppearPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="canReportBugs">if set to <c>true</c> [can report bugs].</param>
        public static void Add(NetworkMessage message, ICharacterSpawn characterSpawn, bool canReportBugs)
        {
            message.AddPacketType(GamePacketType.SelfAppear);
            message.AddUInt32(characterSpawn.Id);

            // TODO: Beat duration (50)
            message.AddUInt16(0x32);

            // TODO: Remove base speeds
            message.AddDouble(857.36, 3);
            message.AddDouble(261.29, 3);
            message.AddDouble(-4795.01, 3);
            message.AddBoolean(canReportBugs);

            // TODO: Can change pvp framing option
            message.AddByte(0x00);

            // TODO: Expert mode button enabled
            message.AddByte(0x00);
        }
    }
}