namespace Tibia.Data
{
    public struct MagicLevel : IMagicLevel
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public byte Current
        {
            get { return (byte) (Base + Bonus); }
        }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the bonus.
        /// </summary>
        /// <value>
        ///     The bonus.
        /// </value>
        public byte Bonus { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the base.
        /// </summary>
        /// <value>
        ///     The base.
        /// </value>
        public byte Base { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        public ulong Experience { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the next level experience.
        /// </summary>
        /// <value>
        ///     The next level experience.
        /// </value>
        public long NextLevelExperience { get; set; }
    }
}