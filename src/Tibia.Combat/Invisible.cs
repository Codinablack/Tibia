using System;
using Tibia.Data;

namespace Tibia.Combat
{
    public class Invisible : CombatBase
    {
        /// <summary>
        ///     Gets or sets the chance.
        /// </summary>
        /// <value>
        ///     The chance.
        /// </value>
        public Percent Chance { get; set; }

        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        /// <value>
        ///     The duration.
        /// </value>
        public TimeSpan Duration { get; set; }
    }
}