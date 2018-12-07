using System;

namespace Tibia.Data
{
    public interface ICombat
    {
        /// <summary>
        ///     Gets or sets the interval.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        TimeSpan Interval { get; set; }
    }
}