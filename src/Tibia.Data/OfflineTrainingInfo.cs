using System;

namespace Tibia.Data
{
    public struct OfflineTrainingInfo : IOfflineTrainingInfo
    {
        /// <summary>
        ///     Gets the time elapsed.
        /// </summary>
        /// <value>
        ///     The time elapsed.
        /// </value>
        public TimeSpan Elapsed { get; set; }
        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        /// <value>
        ///     The skill.
        /// </value>
        public ISkill Skill { get; set; }
    }
}