using Tibia.Data;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class MapDescriptionPacket : MapPacket
    {
        /// <summary>
        ///     Adds the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="position">The position.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        public static void Add(NetworkMessage message, ICharacterSpawn characterSpawn, IVector3 position, CreatureSpawnService creatureSpawnService, TileService tileService)
        {
            message.AddPacketType(GamePacketType.MapDescription);
            message.AddVector3(position);

            // TODO: These magic numbers should not be hard-coded
            AddMapDescription(message, characterSpawn, new Vector3(position.X - 8, position.Y - 6, position.Z), 18, 14, creatureSpawnService, tileService);
        }
    }
}