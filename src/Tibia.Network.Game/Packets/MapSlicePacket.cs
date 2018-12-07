using Tibia.Data;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class MapSlicePacket : MapPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        public static void Add(NetworkMessage message, ICharacterSpawn characterSpawn, IVector3 sourcePosition, IVector3 targetPosition, CreatureSpawnService creatureSpawnService, TileService tileService)
        {
            if (sourcePosition.Y > targetPosition.Y)
            {
                // North, for old X
                message.AddPacketType(GamePacketType.MapSliceNorth);
                AddMapDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, targetPosition.Y - 6, targetPosition.Z), 18, 1, creatureSpawnService, tileService);
            }
            else if (sourcePosition.Y < targetPosition.Y)
            {
                // South, for old X
                message.AddPacketType(GamePacketType.MapSliceSouth);
                AddMapDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, targetPosition.Y + 7, targetPosition.Z), 18, 1, creatureSpawnService, tileService);
            }

            if (sourcePosition.X < targetPosition.X)
            {
                // East, with new Y
                message.AddPacketType(GamePacketType.MapSliceEast);
                AddMapDescription(message, characterSpawn, new Vector3(targetPosition.X + 9, targetPosition.Y - 6, targetPosition.Z), 1, 14, creatureSpawnService, tileService);
            }
            else if (sourcePosition.X > targetPosition.X)
            {
                // West, with new Y
                message.AddPacketType(GamePacketType.MapSliceWest);
                AddMapDescription(message, characterSpawn, new Vector3(targetPosition.X - 8, targetPosition.Y - 6, targetPosition.Z), 1, 14, creatureSpawnService, tileService);
            }
        }
    }
}