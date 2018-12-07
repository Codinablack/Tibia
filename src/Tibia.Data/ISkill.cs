namespace Tibia.Data
{
    public interface ISkill
    {
        /// <summary>
        ///     Gets or sets the base.
        /// </summary>
        /// <value>
        ///     The base.
        /// </value>
        byte Base { get; set; }

        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        byte Current { get; set; }

        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        long Experience { get; set; }

        /// <summary>
        ///     Gets or sets the next level experience.
        /// </summary>
        /// <value>
        ///     The next level experience.
        /// </value>
        long NextLevelExperience { get; set; }

        /// <summary>
        ///     Gets the percent.
        /// </summary>
        /// <returns>The percent.</returns>
        Percent GetPercent();
    }
}