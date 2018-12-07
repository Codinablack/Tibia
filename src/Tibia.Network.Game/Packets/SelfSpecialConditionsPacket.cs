using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class SelfSpecialConditionsPacket : IPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="conditions">The conditions.</param>
        public static void Add(NetworkMessage message, Conditions conditions)
        {
            message.AddPacketType(GamePacketType.SelfConditions);
            message.AddConditions(conditions);
        }
    }
}