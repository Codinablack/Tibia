using Tibia.Data;

namespace Tibia.Items
{
    public class LootItem : ILootItem
    {
        /// <summary>
        ///     Gets or sets the chance.
        /// </summary>
        /// <value>
        ///     The chance.
        /// </value>
        public int Chance { get; set; }
        /// <summary>
        ///     Gets or sets the count maximum.
        /// </summary>
        /// <value>
        ///     The count maximum.
        /// </value>
        public int CountMax { get; set; }
        /// <summary>
        ///     Gets or sets the item identifier.
        /// </summary>
        /// <value>
        ///     The item identifier.
        /// </value>
        public int ItemId { get; set; }
        /// <summary>
        ///     Gets or sets the item.
        /// </summary>
        /// <value>
        ///     The item.
        /// </value>
        public IItem Item { get; set; }
    }
}