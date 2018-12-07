using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Spawns
{
    public class ContainerSpawnFeature : ItemSpawnFeatureBase, IContainerItemSpawn
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Tibia.Spawns.ContainerSpawnFeature" /> class.
        /// </summary>
        public ContainerSpawnFeature()
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

        /// <inheritdoc />
        /// <summary>
        ///     Gets a value indicating whether this instance is full.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is full; otherwise, <c>false</c>.
        /// </value>
        public bool IsFull
        {
            get { return Items.Count == this.GetFeature<IContainer>().Volume; }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        public ICollection<IItemSpawn> Items { get; set; }
    }
}