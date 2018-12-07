using Tibia.Core;

namespace Tibia.Data
{
    public interface ITile
    {
        /// <summary>
        ///     Gets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        IVector3 Position { get; set; }

        /// <summary>
        ///     Gets or sets the tile flags.
        /// </summary>
        /// <value>
        ///     The tile flags.
        /// </value>
        TileFlags Flags { get; set; }


        /// <summary>
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        FloorChangeDirection FloorChangeDirection { get; }

        /// <summary>
        ///     Gets or sets the house identifier.
        /// </summary>
        /// <value>
        ///     The house identifier.
        /// </value>
        uint? HouseId { get; set; }

        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        ObservableCollection<IItemSpawn> Items { get; set; }

        /// <summary>
        ///     Gets or sets the ground.
        /// </summary>
        /// <value>
        ///     The ground.
        /// </value>
        IItemSpawn Ground { get; set; }
    }
}