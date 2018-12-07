using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class TileRemoveArtifactPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackPosition">The stack position.</param>
        public static void Add(NetworkMessage message, IVector3 position, byte stackPosition)
        {
            // TODO: This magic number should not exist here...
            // TODO: Alternatively, we could throw an exception, handle the case before adding this packet, etc.
            if (stackPosition >= 10)
                return;

            message.AddPacketType(GamePacketType.TileRemoveArtifact);
            message.AddVector3(position);
            message.AddByte(stackPosition);
        }
    }
}