using System;

namespace Tibia.Data
{
    public class ItemMovedEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemMovedEventArgs" /> class.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <param name="targetTile">The target tile.</param>
        public ItemMovedEventArgs(IItemSpawn itemSpawn, ITile targetTile)
        {
            ItemSpawn = itemSpawn;
            TargetTile = targetTile;
        }

        /// <summary>
        ///     Gets the item spawn.
        /// </summary>
        /// <value>
        ///     The item spawn.
        /// </value>
        private IItemSpawn ItemSpawn { get; }

        /// <summary>
        ///     Gets the target tile.
        /// </summary>
        /// <value>
        ///     The target tile.
        /// </value>
        private ITile TargetTile { get; }
    }
}