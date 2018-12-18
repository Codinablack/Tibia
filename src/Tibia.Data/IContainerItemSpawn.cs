namespace Tibia.Data
{
    public interface IContainerItemSpawn
    {
        /// <summary>
        ///     Gets a value indicating whether this instance is full.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is full; otherwise, <c>false</c>.
        /// </value>
        bool IsFull { get; }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        /// <value>
        ///     The parent.
        /// </value>
        IContainerItemSpawn Parent { get; set; }

        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        byte Count { get; }

        /// <summary>
        ///     Gets or sets the volume.
        /// </summary>
        /// <value>
        ///     The volume.
        /// </value>
        byte Volume { get; set; }

        /// <summary>
        ///     Attempts to get the item from the collection of items with the specified index.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <returns><c>true</c> if the item is successfully obtained.</returns>
        bool TryGetItemByIndex(byte index, out IItemSpawn itemSpawn);
    }
}