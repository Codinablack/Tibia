using System;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class UnregisteredTileItemEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnregisteredTileItemEventArgs" /> class.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        public UnregisteredTileItemEventArgs(IItemSpawn itemSpawn)
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