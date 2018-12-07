namespace Tibia.Data
{
    public interface IQuestInfo
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
        ///     Gets or sets the quest.
        /// </summary>
        /// <value>
        ///     The quest.
        /// </value>
        IQuest Quest { get; set; }

        /// <summary>
        ///     Gets or sets the quest identifier.
        /// </summary>
        /// <value>
        ///     The quest identifier.
        /// </value>
        int QuestId { get; set; }
    }
}