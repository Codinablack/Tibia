using Tibia.Data;

namespace Tibia.Spawns
{
    public class ItemSpawn : SpawnBase, IItemSpawn
    {
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