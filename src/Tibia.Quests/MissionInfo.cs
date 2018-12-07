using Tibia.Data;

namespace Tibia.Quests
{
    public class MissionInfo : IMissionInfo
    {
        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the mission identifier.
        /// </summary>
        /// <value>
        ///     The mission identifier.
        /// </value>

        public int MissionId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public ICharacterSpawn CharacterSpawn { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the character spawn identifier.
        /// </summary>
        /// <value>
        ///     The character spawn identifier.
        /// </value>
        public int CharacterSpawnId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </value>
        public bool IsComplete { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the mission state identifier.
        /// </summary>
        /// <value>
        ///     The mission state identifier.
        /// </value>
        public int? MissionStateId { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the state of the mission.
        /// </summary>
        /// <value>
        ///     The state of the mission.
        /// </value>
        public IMissionState MissionState { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the mission.
        /// </summary>
        /// <value>
        ///     The mission.
        /// </value>
        public IMission Mission { get; set; }
    }
}