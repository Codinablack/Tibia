using Tibia.Core;

namespace Tibia.Data
{
    public interface IItem : IThing, IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the article.
        /// </summary>
        /// <value>
        ///     The article.
        /// </value>
        string Article { get; set; }

        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        ushort SpriteId { get; set; }

        /// <summary>
        ///     Gets or sets the type of the group.
        /// </summary>
        /// <value>
        ///     The type of the group.
        /// </value>
        ItemGroupType GroupType { get; set; }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        ushort? Speed { get; set; }

        /// <summary>
        ///     Gets or sets the write once item identifier.
        /// </summary>
        /// <value>
        ///     The write once item identifier.
        /// </value>
        ushort? WriteOnceItemId { get; set; }

        /// <summary>
        ///     Gets or sets the floor change direction.
        /// </summary>
        /// <value>
        ///     The floor change direction.
        /// </value>
        FloorChangeDirection FloorChangeDirection { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is pickupable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is pickupable; otherwise, <see langword="false" />.
        /// </value>
        bool IsPickupable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is solid block.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is solid block; otherwise, <see langword="false" />.
        /// </value>
        bool IsSolidBlock { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is projectile blocker.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is projectile blocker; otherwise, <see langword="false" />.
        /// </value>
        bool IsProjectileBlocker { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is path blocker.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is path blocker; otherwise, <see langword="false" />.
        /// </value>
        bool IsPathBlocker { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance has height.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance has height; otherwise, <see langword="false" />.
        /// </value>
        bool HasHeight { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is useable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is useable; otherwise, <see langword="false" />.
        /// </value>
        bool IsUseable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is moveable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is moveable; otherwise, <see langword="false" />.
        /// </value>
        bool IsMoveable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is always on top.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is always on top; otherwise, <see langword="false" />.
        /// </value>
        bool IsAlwaysOnTop { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is vertical.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is vertical; otherwise, <see langword="false" />.
        /// </value>
        bool IsVertical { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is horizontal.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is horizontal; otherwise, <see langword="false" />.
        /// </value>
        bool IsHorizontal { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is hangable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is hangable; otherwise, <see langword="false" />.
        /// </value>
        bool IsHangable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is distance readable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is distance readable; otherwise, <see langword="false" />.
        /// </value>
        bool IsDistanceReadable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is rotatable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is rotatable; otherwise, <see langword="false" />.
        /// </value>
        bool IsRotatable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is readable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is readable; otherwise, <see langword="false" />.
        /// </value>
        bool IsReadable { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is look through.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is look through; otherwise, <see langword="false" />.
        /// </value>
        bool IsLookThrough { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is animation.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is animation; otherwise, <see langword="false" />.
        /// </value>
        bool IsAnimation { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is force use.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is force use; otherwise, <see langword="false" />.
        /// </value>
        bool IsForceUse { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is stackable.
        /// </summary>
        /// <value>
        ///     <see langword="true" /> if this instance is stackable; otherwise, <see langword="false" />.
        /// </value>
        bool IsStackable { get; set; }
    }
}