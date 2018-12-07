using Tibia.Core;

namespace Tibia.Data
{
    public interface IMission : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        ///     <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the quest identifier.
        /// </summary>
        /// <value>
        ///     The quest identifier.
        /// </value>
        int QuestId { get; set; }
    }
}