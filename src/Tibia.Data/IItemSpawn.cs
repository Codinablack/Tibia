namespace Tibia.Data
{
    public interface IItemSpawn : ISpawn
    {
        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        IItem Item { get; set; }

        /// <summary>
        ///     Gets or sets the item identifier.
        /// </summary>
        /// <value>
        ///     The item identifier.
        /// </value>
        int ItemId { get; set; }
    }
}