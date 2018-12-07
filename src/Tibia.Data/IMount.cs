using Tibia.Core;

namespace Tibia.Data
{
    public interface IMount : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is default.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is default; otherwise, <c>false</c>.
        /// </value>
        bool IsDefault { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is god tier.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is god tier; otherwise, <c>false</c>.
        /// </value>
        bool IsGodTier { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is premium.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is premium; otherwise, <c>false</c>.
        /// </value>
        bool IsPremium { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is unlocked.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is unlocked; otherwise, <c>false</c>.
        /// </value>
        bool IsUnlocked { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the speed.
        /// </summary>
        /// <value>
        ///     The speed.
        /// </value>
        ushort Speed { get; set; }

        /// <summary>
        ///     Gets or sets the sprite identifier.
        /// </summary>
        /// <value>
        ///     The sprite identifier.
        /// </value>
        ushort SpriteId { get; set; }
    }
}