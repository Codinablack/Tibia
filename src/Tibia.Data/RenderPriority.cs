namespace Tibia.Data
{
    public enum RenderPriority
    {
        /// <summary>
        ///     The default priority.
        /// </summary>
        None = 0,

        /// <summary>
        ///     The lowest priority.
        /// </summary>
        Lowest = 1,

        /// <summary>
        ///     The low priority.
        /// </summary>
        Low = 2,

        /// <summary>
        ///     The medium low priority.
        /// </summary>
        MediumLow = 3,

        /// <summary>
        ///     The medium
        /// </summary>
        Medium = 4,

        /// <summary>
        ///     The medium high priority.
        /// </summary>
        MediumHigh = 5,

        /// <summary>
        ///     The high priority.
        /// </summary>
        High = 6,

        /// <summary>
        ///     The highest priority.
        /// </summary>
        Highest = 7,

        /// <summary>
        ///     The critical priority.
        /// </summary>
        Critical = 8
    }
}