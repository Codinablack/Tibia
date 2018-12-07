using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Map
{
    public class WorldMap : IWorldMap
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WorldMap" /> class.
        /// </summary>
        public WorldMap()
        {
            Towns = new Dictionary<uint, ITown>();
            Tiles = new Dictionary<IVector3, ITile>();
            Waypoints = new Dictionary<string, IWaypoint>();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the spawn file.
        /// </summary>
        /// <value>
        ///     The spawn file.
        /// </value>
        public string SpawnFile { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the house file.
        /// </summary>
        /// <value>
        ///     The house file.
        /// </value>
        public string HouseFile { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        public ushort Width { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        public ushort Height { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the towns.
        /// </summary>
        /// <value>
        ///     The towns.
        /// </value>
        public IDictionary<uint, ITown> Towns { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the waypoints.
        /// </summary>
        /// <value>
        ///     The waypoints.
        /// </value>
        public IDictionary<string, IWaypoint> Waypoints { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the tiles.
        /// </summary>
        /// <value>
        ///     The tiles.
        /// </value>
        public IDictionary<IVector3, ITile> Tiles { get; set; }
    }
}