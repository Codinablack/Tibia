using System;
using Tibia.Data;

namespace Tibia.Creatures
{
    public class TargetChangeSettings
    {
        /// <summary>
        ///     Gets or sets the chance.
        /// </summary>
        /// <value>
        ///     The chance.
        /// </value>
        public Percent Chance { get; set; }

        /// <summary>
        ///     Gets or sets the interval.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        public TimeSpan Interval { get; set; }
    }
}