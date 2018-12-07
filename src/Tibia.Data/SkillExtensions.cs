using System;

namespace Tibia.Data
{
    public static class SkillExtensions
    {
        /// <summary>
        ///     Converts a specified skill to its equivalent percent.
        /// </summary>
        /// <param name="skill">The skill.</param>
        /// <returns>The percent.</returns>
        public static Percent ToPercent(this ISkill skill)
        {
            if (skill.NextLevelExperience == 0)
                return Percent.MinValue;

            return new Percent((byte) Math.Floor((double) skill.Experience / skill.NextLevelExperience * 100));
        }
    }
}