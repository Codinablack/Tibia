using System.Collections.Generic;
using System.Linq;
using Tibia.Data;
using Tibia.Data.Services;

namespace Tibia.Items
{
    public class ItemService : IService
    {
        private readonly IDictionary<uint, IItem> _itemsById;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemService" /> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public ItemService(IEnumerable<IItem> items)
        {
            _itemsById = items.ToDictionary(s => s.Id);
        }

        /// <summary>
        ///     Gets the item by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>The item.</returns>
        public IItem GetItemById(uint id)
        {
            return _itemsById.ContainsKey(id) ? _itemsById[id] : null;
        }
    }
}