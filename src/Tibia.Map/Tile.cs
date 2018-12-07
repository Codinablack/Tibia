using Tibia.Core;
using Tibia.Data;

namespace Tibia.Map
{
    public class Tile : ITile
    {
        private IItemSpawn _ground;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tile" /> class.
        /// </summary>
        public Tile()
        {
            Items = new ObservableCollection<IItemSpawn>();
            Items.AddedItem += OnAddedItem;
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        public FloorChangeDirection FloorChangeDirection { get; protected set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the position.
        /// </summary>
        /// <value>
        ///     The position.
        /// </value>
        public IVector3 Position { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the tile flags.
        /// </summary>
        /// <value>
        ///     The tile flags.
        /// </value>
        public TileFlags Flags { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the house identifier.
        /// </summary>
        /// <value>
        ///     The house identifier.
        /// </value>
        public uint? HouseId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>
        ///     The items.
        /// </value>
        public ObservableCollection<IItemSpawn> Items { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the ground.
        /// </summary>
        /// <value>
        ///     The ground.
        /// </value>
        public IItemSpawn Ground
        {
            get => _ground;
            set
            {
                _ground = value;
                SetFloorChangeDirection(_ground);
            }
        }

        /// <summary>
        ///     Called when [added item].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="AddedItemEventArgs{IItemSpawn}" /> instance containing the event data.</param>
        private void OnAddedItem(object sender, AddedItemEventArgs<IItemSpawn> e)
        {
            SetFloorChangeDirection(e.Item);
        }

        /// <summary>
        ///     Sets the floor change direction.
        /// </summary>
        /// <param name="itemSpawn">The item spawn.</param>
        private void SetFloorChangeDirection(IItemSpawn itemSpawn)
        {
            if (itemSpawn == null || itemSpawn.Item.FloorChangeDirection == FloorChangeDirection.None)
                return;

            FloorChangeDirection = itemSpawn.Item.FloorChangeDirection;
        }
    }
}