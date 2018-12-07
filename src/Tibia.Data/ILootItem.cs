namespace Tibia.Data
{
    public interface ILootItem
    {
        /// <summary>
        ///     Gets or sets the chance.
        /// </summary>
        /// <value>
        ///     The chance.
        /// </value>
        int Chance { get; set; }

        /// <summary>
        ///     Gets or sets the count maximum.
        /// </summary>
        /// <value>
        ///     The count maximum.
        /// </value>
        int CountMax { get; set; }

        /// <summary>
        ///     Gets or sets the item identifier.
        /// </summary>
        /// <value>
        ///     The item identifier.
        /// </value>
        int ItemId { get; set; }

        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        IItem Item { get; set; }
    }
}