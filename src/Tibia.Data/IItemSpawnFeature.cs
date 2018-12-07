namespace Tibia.Data
{
    public interface IItemSpawnFeature
    {
        /// <summary>
        ///     Gets or sets the item spawn.
        /// </summary>
        /// <value>
        ///     The item spawn.
        /// </value>
        IItemSpawn ItemSpawn { get; set; }
    }
}