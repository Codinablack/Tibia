namespace Tibia.Data
{
    public struct Mana : IMana
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public int Current { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>
        ///     The maximum.
        /// </value>
        public int Maximum { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the spent.
        /// </summary>
        /// <value>
        ///     The spent.
        /// </value>
        public int Spent { get; set; }
    }
}