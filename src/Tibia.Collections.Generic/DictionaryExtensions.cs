using System.Collections.Generic;

namespace Tibia.Collections.Generic
{
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Adds the range.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="items">The items.</param>
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IDictionary<TKey, TValue> items)
        {
            foreach (KeyValuePair<TKey, TValue> item in items)
                dictionary.Add(item);
        }
    }
}