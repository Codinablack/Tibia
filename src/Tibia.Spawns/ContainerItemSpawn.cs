using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class ContainerItemSpawn : ItemSpawn, IContainerItemSpawn
    {
        private readonly Dictionary<byte, IItemSpawn> _items;
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.ContainerItemSpawn" /> class.
        /// </summary>
        public ContainerItemSpawn()
        {
            _items = new Dictionary<byte, IItemSpawn>();
        }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        public IEnumerable<IItemSpawn> Items => _items.Values;
        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        /// <value>
        ///     The parent.
        /// </value>
        public IContainerItemSpawn Parent { get; set; }

        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public byte Count => (byte) _items.Count;

        /// <summary>
        ///     Gets or sets the volume.
        /// </summary>
        /// <value>
        ///     The volume.
        /// </value>
        public byte Volume { get; set; }

        /// <summary>
        ///     Attempts to get the item from the collection of items with the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <returns><c>true</c> if the item is successfully obtained.</returns>
        public bool TryGetItemByIndex(byte index, out IItemSpawn itemSpawn)
        {
            return _items.TryGetValue(index, out itemSpawn);
        }
        /// <summary>
        ///     Gets a value indicating whether this instance is full.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is full; otherwise, <c>false</c>.
        /// </value>
        public bool IsFull => Count == Volume;
    }
}