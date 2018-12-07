using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class ItemSpawn : SpawnBase, IItemSpawn
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.ItemSpawn" /> class.
        /// </summary>
        public ItemSpawn()
        {
            Features = new HashSet<IItemSpawnFeature>();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the features.
        /// </summary>
        /// <value>
        ///     The features.
        /// </value>
        public ICollection<IItemSpawnFeature> Features { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the item identifier.
        /// </summary>
        /// <value>
        ///     The item identifier.
        /// </value>
        public int ItemId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public IItem Item { get; set; }
    }
}