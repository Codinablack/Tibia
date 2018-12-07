using System;

namespace Tibia.Data
{
    public interface ISkull
    {
        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        TimeSpan Time { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        SkullType Type { get; set; }
    }
}