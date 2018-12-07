using System.Collections.Generic;
using Tibia.Core;

namespace Tibia.Data
{
    public interface IGuild : IEntity<ushort>
    {
        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        ///     Gets or sets the ranks.
        /// </summary>
        /// <value>
        ///     The ranks.
        /// </value>
        List<IGuildRank> Ranks { get; set; }
    }
}