using Tibia.Data;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class MapFloorChangeUpPacket : MapPacket
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
            // Floor change up
            message.AddPacketType(GamePacketType.FloorChangeUp);

            // Going to surface
            if (targetPosition.Z == 7)
            {
                int skip = -1;

                // Floor 7 and 6 already set
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, 5), 18, 14, 3, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, 4), 18, 14, 4, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, 3), 18, 14, 5, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, 2), 18, 14, 6, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, 1), 18, 14, 7, creatureSpawnService, tileService, ref skip);
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, 0), 18, 14, 8, creatureSpawnService, tileService, ref skip);

                if (skip >= 0)
                {
                    message.AddByte((byte) skip);
                    message.AddByte(0xFF);
                }
            }

            // Underground, going one floor up (still underground)
            else if (targetPosition.Z > 7)
            {
                int skip = -1;
                AddFloorDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, sourcePosition.Z - 3), 18, 14, 3, creatureSpawnService, tileService, ref skip);

                if (skip >= 0)
                {
                    message.AddByte((byte) skip);
                    message.AddByte(0xFF);
                }
            }

            // Moving up a floor up makes us out of sync
            // Eest
            message.AddPacketType(GamePacketType.MapSliceWest);
            AddMapDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y + 1 - 6, targetPosition.Z), 1, 14, creatureSpawnService, tileService);

            // North
            message.AddPacketType(GamePacketType.MapSliceNorth);
            AddMapDescription(message, characterSpawn, new Vector3(sourcePosition.X - 8, sourcePosition.Y - 6, targetPosition.Z), 18, 1, creatureSpawnService, tileService);
        }
    }
}