using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class CreatureChangeSpeedPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureId">The creature identifier.</param>
        /// <param name="speedInfo">The speed information.</param>
        public static void Add(NetworkMessage message, uint creatureId, SpeedInfo speedInfo)
        {
            message.AddPacketType(GamePacketType.CreatureChangeSpeed);
            message.AddUInt32(creatureId);
            message.AddUInt16(speedInfo.WalkSpeed);
            message.AddUInt16(speedInfo.BonusSpeed);
        }
    }
}