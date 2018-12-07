using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class CreatureMovePacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        public static void Add(NetworkMessage message, IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition)
        {
            message.AddPacketType(GamePacketType.CreatureMove);
            message.AddVector3(sourcePosition);
            message.AddByte(sourceStackPosition);
            message.AddVector3(targetPosition);
        }
    }
}