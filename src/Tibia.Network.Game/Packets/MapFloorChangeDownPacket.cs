using Tibia.Data;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class MapFloorChangeDownPacket : MapPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="sourcePosition">The source position.</param>
        /// <param name="sourceStackPosition">The source stack position.</param>
        /// <param name="targetPosition">The target position.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        public static void Add(NetworkMessage message, ICharacterSpawn characterSpawn, IVector3 sourcePosition, byte sourceStackPosition, IVector3 targetPosition, CreatureSpawnService creatureSpawnService, TileService tileService)
        {
            message.AddPacketType(GamePacketType.FloorChangeDown);
            if (targetPosition.Z == 8)
            {
                // Going from surface to underground
                // TODO: These magic numbers should not be hard-coded
                int skip = -1;
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, targetPosition.Z), 18, 14, -1, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, targetPosition.Z + 1), 18, 14, -2, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, targetPosition.Z + 2), 18, 14, -3, creatureSpawnService, tileService, ref skip);

                if (skip >= 0)
                {
                    message.AddByte((byte) skip);
                    message.AddByte(0xFF);
                }
            }
            else if (targetPosition.Z > sourcePosition.Z && targetPosition.Z > 8 && targetPosition.Z < 14)
            {
                // Going further down
                // TODO: These magic numbers should not be hard-coded
                int skip = -1;
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, targetPosition.Z + 2), 18, 14, -3, creatureSpawnService, tileService, ref skip);

                if (skip >= 0)
                {
                    message.AddByte((byte) skip);
                    message.AddByte(0xFF);
                }
            }

            // Moving down a floor makes us out of sync
            // East
            // TODO: These magic numbers should not be hard-coded
            message.AddPacketType(GamePacketType.MapSliceEast);
            AddMapDescription(message, characterSpawn, new Vector3(sourcePosition.X + 9, sourcePosition.Y - 1 - 6, targetPosition.Z), 1, 14, creatureSpawnService, tileService);

            // South
            // TODO: These magic numbers should not be hard-coded
            message.AddPacketType(GamePacketType.MapSliceSouth);
            AddMapDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y + 7, targetPosition.Z), 18, 1, creatureSpawnService, tileService);
        }
    }
}