using System;
using Tibia.Data;

namespace Tibia.Network.Game.Events
{
    public class RegisteredTileItemEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RegisteredTileItemEventArgs" /> class.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        public RegisteredTileItemEventArgs(IItemSpawn itemSpawn)
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