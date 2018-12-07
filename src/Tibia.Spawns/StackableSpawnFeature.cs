using Tibia.Data;

namespace Tibia.Spawns
{
    public class StackableSpawnFeature : ItemSpawnFeatureBase, IStackableItemSpawn
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public int Count { get; set; }
    }
}