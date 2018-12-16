using Tibia.Data;

namespace Tibia.Items
{
    public class Item : IItem
    {
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
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        public FloorChangeDirection FloorChangeDirection { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pickupable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is pickupable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsPickupable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is solid block.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is solid block; otherwise, <see langword="false" />.
        /// </value>
        public bool IsSolidBlock { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is projectile blocker.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is projectile blocker; otherwise, <see langword="false" />.
        /// </value>
        public bool IsProjectileBlocker { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is path blocker.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is path blocker; otherwise, <see langword="false" />.
        /// </value>
        public bool IsPathBlocker { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has height.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance has height; otherwise, <see langword="false" />.
        /// </value>
        public bool HasHeight { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is useable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is useable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsUseable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is moveable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is moveable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsMoveable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is always on top.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is always on top; otherwise, <see langword="false" />.
        /// </value>
        public bool IsAlwaysOnTop { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is vertical.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is vertical; otherwise, <see langword="false" />.
        /// </value>
        public bool IsVertical { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is horizontal.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is horizontal; otherwise, <see langword="false" />.
        /// </value>
        public bool IsHorizontal { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is hangable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is hangable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsHangable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is distance readable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is distance readable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsDistanceReadable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is rotatable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is rotatable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsRotatable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is readable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is readable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsReadable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is look through.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is look through; otherwise, <see langword="false" />.
        /// </value>
        public bool IsLookThrough { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is animation.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is animation; otherwise, <see langword="false" />.
        /// </value>
        public bool IsAnimation { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is force use.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is force use; otherwise, <see langword="false" />.
        /// </value>
        public bool IsForceUse { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is stackable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is stackable; otherwise, <see langword="false" />.
        /// </value>
        public bool IsStackable { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the render priority.
        /// </summary>
        /// <value>
        ///     The render priority.
        /// </value>
        public RenderPriority RenderPriority { get; set; } = RenderPriority.Lowest;

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