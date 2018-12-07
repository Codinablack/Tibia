﻿namespace Tibia.Data
{
    public class Health : IHealth
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the current.
        /// </summary>
        /// <value>
        ///     The current.
        /// </value>
        public uint Current { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the maximum.
        /// </summary>
        /// <value>
        ///     The maximum.
        /// </value>
        public uint Maximum { get; set; }
    }
}