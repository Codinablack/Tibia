using System.ComponentModel;

namespace Tibia.Data
{
    public class MovingItemEventArgs : CancelEventArgs
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="MovingItemEventArgs" /> class.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        /// <param name="targetTile">The target tile.</param>
        public MovingItemEventArgs(IItemSpawn itemSpawn, ITile targetTile)
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
        public IItemSpawn ItemSpawn { get; }

        /// <summary>
        ///     Gets the target tile.
        /// </summary>
        /// <value>
        ///     The target tile.
        /// </value>
        public ITile TargetTile { get; }
    }
}