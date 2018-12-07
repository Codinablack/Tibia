using System;
using Tibia.Data;

namespace Tibia.Spells
{
    public class SpellBase : ISpell
    {
        /// <summary>
        ///     Gets or sets the interval.
        /// </summary>
        /// <value>
        ///     The interval.
        /// </value>
        /// <inheritdoc />
        public TimeSpan Interval { get; set; }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        /// <inheritdoc />
        public byte Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        /// <inheritdoc />
        public string Name { get; set; }
    }
}