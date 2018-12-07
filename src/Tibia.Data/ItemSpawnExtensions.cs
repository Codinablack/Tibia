namespace Tibia.Data
{
    public static class ItemSpawnExtensions
    {
        /// <summary>
        ///     Gets the feature.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <returns>The feature.</returns>
        public static T GetFeature<T>(this IItemSpawn itemSpawn)
        {
            return itemSpawn.Item.GetFeature<T>();
        }
    }
}