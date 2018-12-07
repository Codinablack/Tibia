using Tibia.Core;

namespace Tibia.Data
{
    public interface IGuildRank : IEntity<uint>
    {
        /// <summary>
        ///     Gets or sets the level.
        /// </summary>
        /// <value>
        ///     The level.
        /// </value>
        byte Level { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }
    }
}