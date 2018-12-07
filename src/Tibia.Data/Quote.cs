using System;

namespace Tibia.Data
{
    public class Quote
    {
        /// <summary>
        ///     Gets or sets the chance.
        /// </summary>
        /// <value>
        ///     The chance.
        /// </value>
        public Percent Chance { get; set; }

        /// <summary>
        ///     Gets or sets the maximum interval.
        /// </summary>
        /// <value>
        ///     The maximum interval.
        /// </value>
        public TimeSpan MaxInterval { get; set; }

        /// <summary>
        ///     Gets or sets the minimum interval.
        /// </summary>
        /// <value>
        ///     The minimum interval.
        /// </value>
        public TimeSpan MinInterval { get; set; }

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        /// <value>
        ///     The message.
        /// </value>
        public string Message { get; set; }
    }
}