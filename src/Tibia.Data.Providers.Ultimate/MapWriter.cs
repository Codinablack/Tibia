using System;
using System.Collections.Generic;
using System.IO;
using Tibia.Map;

namespace Tibia.Data.Providers.Ultimate
{
    public class MapWriter : IDisposable
    {
        private readonly TileService _tileService;
        private readonly BinaryWriter _binaryWriter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MapWriter" /> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="tileService">The tile service.</param>
        public MapWriter(Stream stream, TileService tileService)
        {
            _tileService = tileService;
            _binaryWriter = new BinaryWriter(stream);
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _binaryWriter?.Dispose();
        }

        /// <summary>
        ///     Writes the specified map.
        /// </summary>
        /// <param name="map">The map.</param>
        public void Write(WorldMap map)
        {
            _binaryWriter.WriteString(map.Description);
            _binaryWriter.WriteUInt16(map.Height);
            _binaryWriter.WriteUInt16(map.Width);
            _binaryWriter.WriteString(map.HouseFile);
            _binaryWriter.WriteString(map.SpawnFile);

            foreach (KeyValuePair<uint, ITown> town in map.Towns)
            {
                _binaryWriter.WriteUInt32(town.Value.Id);
                _binaryWriter.WriteString(town.Value.Name);
                _binaryWriter.WriteVector3(town.Value.TemplePosition);
            }

            foreach (KeyValuePair<string, IWaypoint> waypoint in map.Waypoints)
            {
                _binaryWriter.WriteString(waypoint.Value.Name);
                _binaryWriter.WriteVector3(waypoint.Value.Position);
            }

            foreach (KeyValuePair<IVector3, ITile> tile in map.Tiles)
            {
                if (tile.Value.HouseId != null)
                    _binaryWriter.WriteUInt32(tile.Value.HouseId.Value);

                _binaryWriter.WriteInt32((int) tile.Value.Flags);
                _binaryWriter.WriteVector3(tile.Value.Position);

                foreach (IItemSpawn item in _tileService.Items(tile.Key))
                    _binaryWriter.WriteUInt32(item.Item.Id);
            }
        }

        /// <summary>
        ///    Clears all buffers for the current writer and causes any buffered data to be written to the underlying device.
        /// </summary>
        public void Flush()
        {
            _binaryWriter.Flush();
        }
    }
}