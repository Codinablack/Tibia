using Tibia.Data;

namespace Tibia.Quests
{
    public class QuestInfo : IQuestInfo
    {
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
        ///     Gets or sets the character spawn.
        /// </summary>
        /// <value>
        ///     The character spawn.
        /// </value>
        public ICharacterSpawn CharacterSpawn { get; set; }

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the quest identifier.
        /// </summary>
        /// <value>
        ///     The quest identifier.
        /// </value>
        public int QuestId { get; set; }

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
        ///     Gets or sets the quest.
        /// </summary>
        /// <value>
        ///     The quest.
        /// </value>
        public IQuest Quest { get; set; }
    }
}