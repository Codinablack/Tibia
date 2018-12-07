using System;
using Tibia.Data;

namespace Tibia.Combat
{
    public class Paralyze : CombatBase
    {
        /// <summary>
        ///     Gets or sets the duration.
        /// </summary>
        /// <value>
        ///     The duration.
        /// </value>
        public TimeSpan Duration { get; set; }

        /// <summary>
        ///     Gets or sets the change.
        /// </summary>
        /// <value>
        ///     The change.
        /// </value>
        public short Change { get; set; }

        /// <summary>
        ///     Gets or sets the chance.
        /// </summary>
        /// <value>
        ///     The chance.
        /// </value>
        public Percent Chance { get; set; }
    }
}