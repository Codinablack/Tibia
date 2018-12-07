using System;

namespace Tibia.Data
{
    public struct OfflineTrainingInfo : IOfflineTrainingInfo
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets the time elapsed.
        /// </summary>
        /// <value>
        ///     The time elapsed.
        /// </value>
        public TimeSpan Elapsed { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the skill.
        /// </summary>
        /// <value>
        ///     The skill.
        /// </value>
        public ISkill Skill { get; set; }
    }
}