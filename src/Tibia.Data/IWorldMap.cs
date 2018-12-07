using System.Collections.Generic;

namespace Tibia.Data
{
    public interface IWorldMap
    {
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        /// <value>
        ///     The height.
        /// </value>
        ushort Height { get; set; }

        /// <summary>
        ///     Gets or sets the house file.
        /// </summary>
        /// <value>
        ///     The house file.
        /// </value>
        string HouseFile { get; set; }

        /// <summary>
        ///     Gets or sets the spawn file.
        /// </summary>
        /// <value>
        ///     The spawn file.
        /// </value>
        string SpawnFile { get; set; }

        /// <summary>
        ///     Gets or sets the tiles.
        /// </summary>
        /// <value>
        ///     The tiles.
        /// </value>
        IDictionary<IVector3, ITile> Tiles { get; set; }

        /// <summary>
        ///     Gets or sets the towns.
        /// </summary>
        /// <value>
        ///     The towns.
        /// </value>
        IDictionary<uint, ITown> Towns { get; set; }

        /// <summary>
        ///     Gets or sets the waypoints.
        /// </summary>
        /// <value>
        ///     The waypoints.
        /// </value>
        IDictionary<string, IWaypoint> Waypoints { get; set; }

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        /// <value>
        ///     The width.
        /// </value>
        ushort Width { get; set; }
    }
}