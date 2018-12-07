using System;
using Tibia.Data;
using Tibia.Map;
using Tibia.Spawns;

namespace Tibia.Network.Game.Packets
{
    public class MapPacket : CreaturePacket
    {
        /// <summary>
        ///     Adds the map description.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="position">The position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        public static void AddMapDescription(NetworkMessage message, ICharacterSpawn characterSpawn, Vector3 position, ushort width, ushort height, CreatureSpawnService creatureSpawnService, TileService tileService)
        {
            // TODO: These magic numbers should not be hard-coded
            int skip = -1;
            int startZ;
            int endZ;
            int zStep;

            if (position.Z > 7)
            {
                startZ = position.Z - 2;
                // TODO: endZ = Math.Min(WorldMap.MaxLayers - 1, position.Z + 2);
                endZ = Math.Min(15, position.Z + 2);
                zStep = 1;
            }
            else
            {
                startZ = 7;
                endZ = 0;
                zStep = -1;
            }

            for (int nz = startZ; nz != endZ + zStep; nz += zStep)
                AddFloorDescription(message, characterSpawn, new Vector3(position.X, position.Y, nz), width, height, position.Z - nz, creatureSpawnService, tileService, ref skip);

            if (skip >= 0)
            {
                message.AddByte((byte) skip);
                message.AddByte(0xFF);
            }
        }

        /// <summary>
        ///     Adds the floor description.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="position">The position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        /// <param name="tileService">The tile service.</param>
        /// <param name="skip">The skip.</param>
        public static void AddFloorDescription(NetworkMessage message, ICharacterSpawn characterSpawn, Vector3 position, int width, int height, int offset, CreatureSpawnService creatureSpawnService, TileService tileService, ref int skip)
        {
            // TODO: These magic numbers should not be hard-coded!!!!!
            for (int nx = 0; nx < width; nx++)
            {
                for (int ny = 0; ny < height; ny++)
                {
                    Vector3 tilePosition = new Vector3(position.X + nx + offset, position.Y + ny + offset, position.Z);
                    ITile tile = tileService.GetTileByPosition(tilePosition);
                    if (tile != null)
                    {
                        if (skip >= 0)
                        {
                            message.AddByte((byte) skip);
                            message.AddByte(0xFF);
                        }
                        skip = 0;

                        AddTileDescription(message, characterSpawn, tile, tileService, creatureSpawnService);
                    }
                    else
                    {
                        skip++;
                        if (skip == 0xFF)
                        {
                            message.AddByte(0xFF);
                            message.AddByte(0xFF);
                            skip = -1;
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Adds the tile description.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="characterSpawn">The character spawn.</param>
        /// <param name="tile">The tile.</param>
        /// <param name="tileService">The tile service.</param>
        /// <param name="creatureSpawnService">The creature spawn service.</param>
        public static void AddTileDescription(NetworkMessage message, ICharacterSpawn characterSpawn, ITile tile, TileService tileService, CreatureSpawnService creatureSpawnService)
        {
            // TODO: This should probably throw an exception
            if (tile == null)
                return;

            // Environmental effects
            // TODO: These magic numbers should not be hard-coded
            message.AddUInt16(0x00);

            int count;
            if (tile.Ground != null)
            {
                message.AddItemSpawn(tile.Ground);
                // TODO: These magic numbers should not be hard-coded
                count = 1;
            }
            else
            {
                // TODO: These magic numbers should not be hard-coded
                count = 0;
            }

            foreach (IItemSpawn item in tile.GetItemsBeforeMediumPriority())
            {
                message.AddItemSpawn(item);
                // TODO: These magic numbers should not be hard-coded
                if (++count == 10)
                    return;
            }

            foreach (ICreatureSpawn creature in tileService.GetCreaturesByPosition(tile.Position))
            {
                if (!characterSpawn.CanSee(creature))
                    continue;

                bool known = characterSpawn.Connection.IsCreatureKnown(creature, creatureSpawnService, out uint removeKnown);
                AddCreature(message, characterSpawn, creature, known, removeKnown);

                // TODO: These magic numbers should not be hard-coded
                if (++count == 10)
                    return;
            }

            foreach (IItemSpawn item in tile.GetItemsAfterMediumPriority())
            {
                message.AddItemSpawn(item);
                // TODO: These magic numbers should not be hard-coded
                if (++count == 10)
                    return;
            }
        }
    }
}