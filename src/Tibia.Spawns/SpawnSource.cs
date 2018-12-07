using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class SpawnSource
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SpawnSource" /> class.
        /// </summary>
        public SpawnSource()
        {
            Spawns = new HashSet<SpawnSettingsBase>();
        }

        /// <summary>
        ///     Gets or sets the center position.
        /// </summary>
        /// <value>
        ///     The center position.
        /// </value>
        public IVector3 CenterPosition { get; set; }

        /// <summary>
        ///     Gets or sets the spawns.
        /// </summary>
        /// <value>
        ///     The spawns.
        /// </value>
        public ICollection<SpawnSettingsBase> Spawns { get; set; }

        /// <summary>
        ///     Gets or sets the radius.
        /// </summary>
        /// <value>
        ///     The radius.
        /// </value>
        public int Radius { get; set; }
    }
}