using System.Linq;

namespace Tibia.Data
{
    public static class ItemExtensions
    {
        /// <summary>
        ///     Gets the feature.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">The item.</param>
        /// <returns>The feature.</returns>
        public static T GetFeature<T>(this IItem item)
        {
            return (T) item.Features.FirstOrDefault(s => s is T);
        }
    }
}