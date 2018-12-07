using Tibia.Data;

namespace Tibia.Spawns
{
    public abstract class ItemSpawnFeatureBase : IItemSpawnFeature
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the item spawn.
        /// </summary>
        /// <value>
        ///     The item spawn.
        /// </value>
        public IItemSpawn ItemSpawn { get; set; }
    }
}