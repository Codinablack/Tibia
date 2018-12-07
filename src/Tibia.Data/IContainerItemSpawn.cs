using System.Collections.Generic;

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
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        ICollection<IItemSpawn> Items { get; set; }

        /// <summary>
        ///     Gets or sets the parent.
        /// </summary>
        /// <value>
        ///     The parent.
        /// </value>
        IContainerItemSpawn Parent { get; set; }
    }
}