namespace Tibia.Data
{
    public struct Percent
    {
        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public byte Value { get; }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Percent" /> struct.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="InvalidPercentException">Percent value cannot be less than <see cref="MinValue" /></exception>
        /// <exception cref="InvalidPercentException">
        ///     Percent value cannot be greater than <see cref="MaxValue" />
        /// </exception>
        public Percent(byte value)
        {
            if (value > 100)
                throw new InvalidPercentException("Percent value cannot be greater than Percent.MaxValue");

            Value = value;
        }

        /// <summary>
        ///     The maximum value.
        /// </summary>
        public static Percent MaxValue = new Percent(100);

        /// <summary>
        ///     The minimum value.
        /// </summary>
        public static Percent MinValue = new Percent(0);
    }
}