namespace Tibia.Data
{
    public interface IMissionInfo
    {
        /// <summary>
        ///     Gets or sets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        ICharacterSpawn CharacterSpawn { get; set; }

        /// <summary>
        ///     Gets or sets the character spawn identifier.
        /// </summary>
        /// <value>
        ///     The character spawn identifier.
        /// </value>
        int CharacterSpawnId { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is complete.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is complete; otherwise, <c>false</c>.
        /// </value>
        bool IsComplete { get; set; }

        /// <summary>
        ///     Gets or sets the mission identifier.
        /// </summary>
        /// <value>
        ///     The mission identifier.
        /// </value>
        int MissionId { get; set; }

        /// <summary>
        ///     Gets or sets the mission state identifier.
        /// </summary>
        /// <value>
        ///     The mission state identifier.
        /// </value>
        int? MissionStateId { get; set; }

        /// <summary>
        ///     Gets or sets the state of the mission.
        /// </summary>
        /// <value>
        ///     The state of the mission.
        /// </value>
        IMissionState MissionState { get; set; }

        /// <summary>
        ///     Gets or sets the mission.
        /// </summary>
        /// <value>
        ///     The mission.
        /// </value>
        IMission Mission { get; set; }
    }
}