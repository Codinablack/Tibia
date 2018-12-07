﻿using Tibia.Data;
using System;

namespace Tibia.Battle
{
    public class Skull : ISkull
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public SkullType Type { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the time.
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public TimeSpan Time { get; set; }
    }
}