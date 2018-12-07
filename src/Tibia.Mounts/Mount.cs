using Tibia.Data;

namespace Tibia.Mounts
{
    public class Mount : IMount
    {
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
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        public ushort Speed { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is premium.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        public bool IsPremium { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is unlocked.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is unlocked; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnlocked { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is god tier.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is god tier; otherwise, <c>false</c>.
        /// </value>
        public bool IsGodTier { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        public bool IsDefault { get; set; }

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