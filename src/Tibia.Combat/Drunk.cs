using System;

namespace Tibia.Combat
{
    /// <summary>
    ///     Represents a distance attack causes the target to immediately become drunk. By default, the effect lasts up to 200
    ///     seconds on the target.
    /// </summary>
    /// <seealso cref="CombatBase" />
    public class Drunk : CombatBase
    {
        /// <summary>
        ///     Gets the duration.
        /// </summary>
        /// <value>
        ///     The duration.
        /// </value>
        public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(200);
    }
}