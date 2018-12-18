using Tibia.Data;

namespace Tibia.Quests
{
    public class Mission : IMission
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }
        /// <summary>
        ///     Gets or sets the quest identifier.
        /// </summary>
        /// <value>
        ///     The quest identifier.
        /// </value>
        public int QuestId { get; set; }
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>
        ///     The identifier.
        /// </value>
        public uint Id { get; set; }
    }
}