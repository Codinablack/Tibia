namespace Tibia.Data
{
    public static class ItemSpawnFeatureExtensions
    {
        /// <summary>
        ///     Gets the feature.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemSpawnFeature">The item spawn feature.</param>
        /// <returns>The feature.</returns>
        public static T GetFeature<T>(this IItemSpawnFeature itemSpawnFeature)
        {
            return itemSpawnFeature.ItemSpawn.GetFeature<T>();
        }
    }
}