namespace Tibia.Data
{
    public struct Skill : ISkill
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public byte Current { get; set; }

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
        public long Experience { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the next level experience.
        /// </summary>
        /// <value>
        ///     The next level experience.
        /// </value>
        public long NextLevelExperience { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the percent.
        /// </summary>
        /// <returns>The percent.</returns>
        public Percent GetPercent()
        {
            if (NextLevelExperience == 0)
                return Percent.MinValue;

            return new Percent((byte) (Experience / NextLevelExperience * 100));
        }
    }
}