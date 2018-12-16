using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class ContainerItemSpawn : ItemSpawn, IContainerItemSpawn
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.ContainerItemSpawn" /> class.
        /// </summary>
        public ContainerItemSpawn()
        {
            Items = new HashSet<IItemSpawn>();
        }

        /// <inheritdoc />
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
        public byte Count => (byte) Items.Count;

        /// <summary>
        ///     Gets or sets the volume.
        /// </summary>
        /// <value>
        ///     The volume.
        /// </value>
        public byte Volume { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether this instance is full.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is full; otherwise, <c>false</c>.
        /// </value>
        public bool IsFull => Items.Count == Volume;

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        protected ICollection<IItemSpawn> Items { get; }
    }
}