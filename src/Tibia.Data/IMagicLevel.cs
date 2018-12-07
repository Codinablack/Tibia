namespace Tibia.Data
{
    public interface IMagicLevel
    {
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        byte Current { get; }

        /// <summary>
        ///     Gets or sets the bonus.
        /// </summary>
        /// <value>
        ///     The bonus.
        /// </value>
        byte Bonus { get; set; }

        /// <summary>
        ///     Gets or sets the base.
        /// </summary>
        /// <value>
        ///     The base.
        /// </value>
        byte Base { get; set; }

        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        ulong Experience { get; set; }

        /// <summary>
        ///     Gets or sets the next level experience.
        /// </summary>
        /// <value>
        ///     The next level experience.
        /// </value>
        long NextLevelExperience { get; set; }
    }
}