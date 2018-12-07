using System;

namespace Tibia.Data
{
    public static class ManaExtensions
    {
        /// <summary>
        ///     Converts a specified mana to its equivalent percent.
        /// </summary>
        /// <param name="mana">The mana.</param>
        /// <returns>The percent.</returns>
        public static Percent ToPercent(this Mana mana)
        {
            if (mana.Current == 0 || mana.Maximum == 0)
                return Percent.MinValue;

            return new Percent((byte) Math.Floor((double) mana.Current / mana.Maximum * 100));
        }
    }
}