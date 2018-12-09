using System.Collections.Generic;

namespace Tibia.Windows.Console
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        ///     Adds the range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="items">The items.</param>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
                collection.Add(item);
        }
    }
}