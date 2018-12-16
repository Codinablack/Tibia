using Tibia.Data;

namespace Tibia.Spawns
{
    public class StackableItemSpawn : ItemSpawn, IStackableItemSpawn
    {
        /// <summary>
        ///     Gets or sets the count.
        /// </summary>
        /// <value>
        ///     The count.
        /// </value>
        public int Count { get; set; }
    }
}