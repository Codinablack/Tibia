using Tibia.Data;

namespace Tibia.Quests
{
    public class MissionState : IMissionState
    {
        /// <summary>
        ///     Gets or sets the mission identifier.
        /// </summary>
        /// <value>
        ///     The mission identifier.
        /// </value>
        public int MissionId { get; set; }
        /// <summary>
        ///     Gets or sets the mission.
        /// </summary>
        /// <value>
        ///     The mission.
        /// </value>
        public IMission Mission { get; set; }
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        public string Description { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
    }
}