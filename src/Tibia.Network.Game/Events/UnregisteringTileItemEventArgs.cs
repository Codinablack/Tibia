using System.ComponentModel;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class UnregisteringTileItemEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnregisteringTileItemEventArgs" /> class.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        public UnregisteringTileItemEventArgs(IItemSpawn itemSpawn)
        {
            ItemSpawn = itemSpawn;
        }

        /// <summary>
        ///     Gets the item spawn.
        /// </summary>
        /// <value>
        ///     The item spawn.
        /// </value>
        public IItemSpawn ItemSpawn { get; }
    }
}