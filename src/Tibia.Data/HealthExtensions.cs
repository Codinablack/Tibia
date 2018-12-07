using System;

namespace Tibia.Data
{
    public static class HealthExtensions
    {
        /// <summary>
        ///     Converts a specified health to its equivalent percent.
        /// </summary>
        /// <param name="health">The health.</param>
        /// <returns>The percent.</returns>
        public static Percent ToPercent(this IHealth health)
        {
            if (health.Current == 0 || health.Maximum == 0)
                return Percent.MinValue;

            return new Percent((byte) Math.Floor((double) health.Current / health.Maximum * 100));
        }
    }
}