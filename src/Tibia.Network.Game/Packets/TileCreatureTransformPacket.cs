using Tibia.Data;

namespace Tibia.Network.Game.Packets
{
    public class TileCreatureTransformPacket : IServerPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="creatureSpawn">The creature spawn.</param>
        public static void Add(NetworkMessage message, ICreatureSpawn creatureSpawn)
        {
            message.AddPacketType(GamePacketType.TileArtifactTransform);
            message.AddVector3(creatureSpawn.Tile.Position);
            message.AddByte(creatureSpawn.StackPosition);

            // TODO: Find out what this value means
            message.AddUInt16(0x63);
            message.AddUInt32(creatureSpawn.Id);
            message.AddDirection(creatureSpawn.Direction);

            // TODO: characterSpawn.CanWalkThrough(creatureSpawn)
            message.AddBoolean(false);
        }
    }
}