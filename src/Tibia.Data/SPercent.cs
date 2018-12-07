namespace Tibia.Data
{
    public struct SPercent
    {
        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public sbyte Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SPercent" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidPercentException">
        ///     Percent value cannot be less than <see cref="SPercent.MinValue" />
        /// </exception>
        /// <exception cref="InvalidPercentException">
        ///     Percent value cannot be greater than <see cref="SPercent.MaxValue" />
        /// </exception>
        public SPercent(sbyte value)
        {
            if (value > MaxValue.Value)
                throw new InvalidPercentException("Percent value cannot be greater than Percent.MaxValue");

            if (value < MinValue.Value)
                throw new InvalidPercentException("Percent value cannot be less than Percent.MinValue");

            Value = value;
        }

        /// <summary>
        ///     The maximum value.
        /// </summary>
        public static SPercent MaxValue = new SPercent(100);

        /// <summary>
        ///     The minimum value.
        /// </summary>
        public static SPercent MinValue = new SPercent(-100);
    }
}