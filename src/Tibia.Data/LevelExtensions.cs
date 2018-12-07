using System;

namespace Tibia.Data
{
    public static class LevelExtensions
    {
        /// <summary>
        ///     Converts a specified level to its equivalent percent.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <returns>The percent.</returns>
        public static Percent ToPercent(this ILevel level)
        {
            if (level.NextLevelExperience == 0)
                return Percent.MinValue;

            return new Percent((byte) Math.Floor((double) level.Experience / level.NextLevelExperience * 100));
        }
    }
}