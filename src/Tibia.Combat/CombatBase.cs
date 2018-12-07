using System;
using Tibia.Data;

namespace Tibia.Combat
{
    public abstract class CombatBase : ICombat
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the interval.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        public TimeSpan Interval { get; set; }
    }
}