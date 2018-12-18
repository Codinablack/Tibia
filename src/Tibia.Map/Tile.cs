using Tibia.Data;

namespace Tibia.Map
{
    public class Tile : ITile
    {
        /// <summary>
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        public FloorChangeDirection FloorChangeDirection { get; set; }
        /// <summary>
        ///     Gets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public IVector3 Position { get; set; }
        /// <summary>
        ///     Gets or sets the tile flags.
        /// </summary>
        /// <value>
        ///     The tile flags.
        /// </value>
        public TileFlags Flags { get; set; }
        /// <summary>
        ///     Gets or sets the house identifier.
        /// </summary>
        /// <value>
        ///     The house identifier.
        /// </value>
        public uint? HouseId { get; set; }
        /// <summary>
        ///     Gets or sets the ground.
        /// </summary>
        /// <value>
        ///     The ground.
        /// </value>
        public IItemSpawn Ground { get; set; }
    }
}