using System.Collections.Generic;

namespace Tibia.Data.Providers.Ultimate
{
    public class House : Map.House
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="House" /> class.
        /// </summary>
        public House()
        {
            Tiles = new HashSet<ITile>();
        }

        /// <summary>
        ///     Gets or sets the owner.
        /// </summary>
        /// <value>
        ///     The owner.
        /// </value>
        public ICharacterSpawn Owner { get; set; }

        /// <summary>
        ///     Gets or sets the tiles.
        /// </summary>
        /// <value>
        ///     The tiles.
        /// </value>
        public ICollection<ITile> Tiles { get; set; }
    }
}