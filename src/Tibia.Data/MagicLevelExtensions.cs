using System;

namespace Tibia.Data
{
    public static class MagicLevelExtensions
    {
        /// <summary>
        ///     Converts a specified magic level to its equivalent percent.
        /// </summary>
        /// <param name="magicLevel">The magic level.</param>
        /// <returns>The percent.</returns>
        public static Percent ToPercent(this IMagicLevel magicLevel)
        {
            if (magicLevel.NextLevelExperience == 0)
                return Percent.MinValue;

            return new Percent((byte) Math.Floor((double) magicLevel.Experience / magicLevel.NextLevelExperience * 100));
        }
    }
}