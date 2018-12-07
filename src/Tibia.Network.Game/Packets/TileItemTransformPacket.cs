using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class TileItemTransformPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="vector3">The vector3.</param>
        /// <param name="stackPosition">The stack position.</param>
        /// <param name="itemSpawn">The item spawn.</param>
        public static void Add(NetworkMessage message, Vector3 vector3, byte stackPosition, ItemSpawn itemSpawn)
        {
            message.AddPacketType(GamePacketType.TileArtifactTransform);
            message.AddVector3(vector3);
            message.AddByte(stackPosition);
            message.AddItemSpawn(itemSpawn);
        }
    }
}