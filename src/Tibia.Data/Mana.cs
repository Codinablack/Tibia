namespace Tibia.Data
{
    public struct Mana : IMana
    {
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public int Current { get; set; }
        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>
        ///     The maximum.
        /// </value>
        public int Maximum { get; set; }
        /// <summary>
        ///     Gets or sets the spent.
        /// </summary>
        /// <value>
        ///     The spent.
        /// </value>
        public int Spent { get; set; }
    }
}