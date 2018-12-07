using System;

namespace Tibia.Data
{
    public interface IOfflineTrainingInfo
    {
        /// <summary>
        ///     Gets the elapsed.
        /// </summary>
        /// <value>
        ///     The elapsed.
        /// </value>
        TimeSpan Elapsed { get; set; }

        /// <summary>
        ///     Gets the skill.
        /// </summary>
        /// <value>
        ///     The skill.
        /// </value>
        ISkill Skill { get; set; }
    }
}