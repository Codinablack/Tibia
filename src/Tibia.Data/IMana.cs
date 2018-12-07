namespace Tibia.Data
{
    public interface IMana
    {
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        int Current { get; set; }

        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>
        ///     The maximum.
        /// </value>
        int Maximum { get; set; }

        /// <summary>
        ///     Gets or sets the spent.
        /// </summary>
        /// <value>
        ///     The spent.
        /// </value>
        int Spent { get; set; }
    }
}