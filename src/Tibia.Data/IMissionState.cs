using Tibia.Core;

namespace Tibia.Data
{
    public interface IMissionState : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        /// <value>
        ///     The description.
        /// </value>
        string Description { get; set; }

        /// <summary>
        ///     Gets or sets the mission.
        /// </summary>
        /// <value>
        ///     The mission.
        /// </value>
        IMission Mission { get; set; }

        /// <summary>
        ///     Gets or sets the mission identifier.
        /// </summary>
        /// <value>
        ///     The mission identifier.
        /// </value>
        int MissionId { get; set; }
    }
}