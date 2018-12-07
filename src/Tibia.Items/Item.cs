using System.Collections.Generic;
using Tibia.Data;

namespace Tibia.Items
{
    public class Item : IItem
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Item" /> class.
        /// </summary>
        public Item()
        {
            Features = new HashSet<IItemFeature>();
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the article.
        /// </summary>
        /// <value>
        ///     The article.
        /// </value>
        public string Article { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        public ushort SpriteId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the type of the group.
        /// </summary>
        /// <value>
        ///     The type of the group.
        /// </value>
        public ItemGroupType GroupType { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        public ushort? Speed { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the write once item identifier.
        /// </summary>
        /// <value>
        ///     The write once item identifier.
        /// </value>
        public ushort? WriteOnceItemId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the features.
        /// </summary>
        /// <value>
        ///     The features.
        /// </value>
        public ICollection<IItemFeature> Features { get; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        public FloorChangeDirection FloorChangeDirection { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the render priority.
        /// </summary>
        /// <value>
        ///     The render priority.
        /// </value>
        public RenderPriority RenderPriority { get; protected set; } = RenderPriority.Lowest;

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
    }
}