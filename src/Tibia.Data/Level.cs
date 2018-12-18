namespace Tibia.Data
{
    public class Level : ILevel
    {
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public int Current { get; set; }
        /// <summary>
        ///     Gets or sets the experience.
        /// </summary>
        /// <value>
        ///     The experience.
        /// </value>
        public ulong Experience { get; set; }
        /// <summary>
        ///     Gets or sets the next level experience.
        /// </summary>
        /// <value>
        ///     The next level experience.
        /// </value>
        public long NextLevelExperience { get; set; }
    }
}